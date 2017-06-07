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
	/// FUnNormalShip 的摘要说明。
	/// </summary>
	public class FUnNormalShip : System.Windows.Forms.Form
	{
		#region 变量声明部分

		private const string NormalShip = "正常品出货";
		private const string UnNormalShip = "非正常品出货";

		private DataTable dtFIFO = new DataTable();

		protected System.Windows.Forms.Panel panel1;
		protected UserControl.UCButton ucBtnExit;
		protected System.Windows.Forms.Panel panel2;
		protected UserControl.UCLabelEdit ucLETicketNo;

		protected DataTable _tmpTable = new DataTable();
		protected UserControl.UCLabelEdit txtSumNum;

		protected System.Windows.Forms.Panel panel4;
		protected System.ComponentModel.Container components = null;

		protected InventoryFacade _facade = null;

		protected IDomainDataProvider _domainDataProvider;
		protected UserControl.UCButton btnDeleteRCard;
		protected UserControl.UCButton btnDeleteDetail;
		protected Infragistics.Win.UltraWinGrid.UltraGrid ultraGridContent;
		protected UserControl.UCButton ucBtnComplete;
		protected System.Windows.Forms.Panel pnlAbnormal;
		protected UserControl.UCLabelEdit txtItemDesc;
		protected UserControl.UCLabelCombox cbxItemCode;
		protected UserControl.UCLabelCombox cbxModel;
		protected System.Windows.Forms.GroupBox gbxDetail;
		private UserControl.UCLabelCombox cbxType;
		private UserControl.UCLabelEdit txtDesc;
		private UserControl.UCLabelEdit txtPartner;
		private UserControl.UCDatetTime dateShip;
		private UserControl.UCLabelEdit txtPlanQty;
		private UserControl.UCButton btnAdd;
		//protected DataTable _tblRCard = new DataTable();
		private string _currseq;
		FInfoForm _infoForm = ApplicationRun.GetInfoForm();
		protected UserControl.UCButton btnUnComplete;
		protected UserControl.UCLabelEdit ucLEInput;
		protected UserControl.UCLabelEdit txtMoCode;
		protected UserControl.UCLabelEdit txtCartonNum;
		private System.Windows.Forms.RadioButton rbDelete;
		private System.Windows.Forms.RadioButton rbAdd;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblCheck;
		private System.Windows.Forms.GroupBox grpFIFO;
		protected Infragistics.Win.UltraWinGrid.UltraGrid ultraGridFIFO;
		protected UserControl.UCButton btnCheck;
		protected System.Windows.Forms.GroupBox groupBox1;
		protected Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOsType;
		protected UserControl.UCLabelCombox txtShipType;
		protected UserControl.UCLabelEdit txtDay;
		private System.Windows.Forms.CheckBox cbFIFO;
		protected UserControl.UCButton btnOutputFIFO;
		private string _shipNo;
		#endregion

		#region 系统部分（构造函数，系统服务）
		public FUnNormalShip()
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
		
	
		
		private BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider DataProvider
		{
			get
			{
				return (BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider;
			}
		}

		protected void SucessMessage(string msg)
		{
				this._infoForm.Add(new UserControl.Message(UserControl.MessageType.Success,msg));
		}

		protected void ErrorMessage(string msg)
		{			
			this._infoForm.Add(new UserControl.Message(UserControl.MessageType.Error,msg));
			BenQGuru.eMES.Web.Helper.SoundPlayer.PlayErrorMusic();
		}

		private void FUnNormalShip_Closed(object sender, System.EventArgs e)
		{
			if (this.DataProvider!=null)
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();  
			}
		}

		private void CloseConnection()
		{
			if (this._domainDataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this._domainDataProvider).PersistBroker.CloseConnection(); 
		}

		private void FUnNormalShip_Load(object sender, System.EventArgs e)
		{
			this._infoForm.Add("");
			_domainDataProvider =ApplicationService.Current().DataProvider;

			this._facade = new InventoryFacade( this.DataProvider );
			UserControl.UIStyleBuilder.FormUI(this);
			
			ucLETicketNo.InnerTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;

			#region 初始化Grid
			UserControl.UIStyleBuilder.GridUI(this.ultraGridContent);
			UserControl.UIStyleBuilder.GridUI(this.ultraGridFIFO);

			_tmpTable.Columns.Clear();
			_tmpTable.Columns.Add("*",typeof(bool));
			_tmpTable.Columns.Add("NO",typeof(string));
			_tmpTable.Columns.Add("SEQ",typeof(int));
			_tmpTable.Columns.Add( "CustomerCode", typeof( string ));
			_tmpTable.Columns.Add( "ShipDate", typeof( string ));
			_tmpTable.Columns.Add( "ModelCode", typeof( string ));
			_tmpTable.Columns.Add( "ItemCode", typeof( string ));
			_tmpTable.Columns.Add( "ItemDesc", typeof( string ));
			_tmpTable.Columns.Add( "PlanQty", typeof( string ) );
			_tmpTable.Columns.Add( "ActQty", typeof( string ) );
			_tmpTable.Columns.Add( "status", typeof( string ) );
			BindGrid();

			InitialFIFOGrid();

//			UserControl.UIStyleBuilder.GridUI(this.gridRCard);
//			this._tblRCard.Columns.Clear();
//			this._tblRCard.Columns.Add("RCard",typeof(string));
//			this._tblRCard.Columns.Add("CollectType",typeof(string));
//			this._tblRCard.Columns.Add("SEQ",typeof(string));

			//System.Data.DataView dv = new System.Data.DataView(this._tblRCard,"SEQ=" + 0,string.Empty,System.Data.DataViewRowState.CurrentRows);
			//BindRCard(dv);

			this.dateShip.Value = DateTime.Now;
			
			//出货单类型
			this.cbxType.ComboBoxData.Items.Clear();
			object[] types= new BenQGuru.eMES.BaseSetting.SystemSettingFacade(this.DataProvider).GetParametersByParameterGroup("INVSHIP");
			if(types != null && types.Length > 0)
			{
				foreach(object obj in types)
				{
					BenQGuru.eMES.Domain.BaseSetting.Parameter param = obj as BenQGuru.eMES.Domain.BaseSetting.Parameter;
					if(obj != null)
					{
						this.cbxType.ComboBoxData.Items.Add(param.ParameterCode);
					}
					
				}
			}
			if(this.cbxType.ComboBoxData.Items.Count > 0)
				this.cbxType.ComboBoxData.SelectedIndex = 0;

			BindModel();
			#endregion

			#region 检验对象的类型
			//默认选择为二维条码
			ultraOsType.Items.Clear();
			ultraOsType.CheckedItem =  ultraOsType.Items.Add(CollectionType.Carton.ToString(),CollectionType.Carton.ToString());
			//this.ultraOptionSetOQCExameOpion.Items.Add(OQCFacade.OQC_ExameObject_Carton,"Carton");
			ultraOsType.Items.Add(CollectionType.PCS.ToString(),CollectionType.PCS.ToString());
		
			#endregion

			txtShipType.ComboBoxData.Items.Add(NormalShip);
			txtShipType.ComboBoxData.Items.Add(UnNormalShip);

			txtShipType.SelectedIndex = 0;

			this.ucLETicketNo.TextFocus(false, true);
		}

		private void InitialFIFOGrid()
		{
			dtFIFO.Columns.Clear();
		
			dtFIFO.Columns.Add( "CartonCode", typeof( string )).ReadOnly = true;
			dtFIFO.Columns.Add( "Rcard", typeof( string )).ReadOnly = true;
			dtFIFO.Columns.Add( "ModelCode", typeof( string )).ReadOnly = true;
			dtFIFO.Columns.Add( "ItemCode", typeof( string )).ReadOnly = true;
			dtFIFO.Columns.Add( "RecDate", typeof( string )).ReadOnly = true;
			dtFIFO.Columns.Add( "RecTime", typeof( string )).ReadOnly = true;
			
		}

		private void ultraGridContent_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
		{
			UltraWinGridHelper gridHelper = new UltraWinGridHelper(this.ultraGridContent);
			gridHelper.AddCommonColumn("*","*");
			gridHelper.AddCommonColumn("NO","NO");
			gridHelper.AddCommonColumn("SEQ","项次");
			gridHelper.AddCommonColumn("CustomerCode","提货方");
			gridHelper.AddCommonColumn("ShipDate","发货日期");
			gridHelper.AddCommonColumn("ModelCode","产品别");
			gridHelper.AddCommonColumn("ItemCode","产品代码");
			gridHelper.AddCommonColumn("ItemDesc","产品描述");
			gridHelper.AddCommonColumn("PlanQty","计划数量");
			gridHelper.AddCommonColumn("ActQty","实际数量");
			gridHelper.AddCommonColumn("status","status");
		}

		private void gridRCard_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
		{
//			UltraWinGridHelper gridHelper = new UltraWinGridHelper(this.gridRCard);
//			gridHelper.AddCommonColumn("RCard","序列号");
//			gridHelper.AddCommonColumn("CollectType","CollectType");
//			gridHelper.AddCommonColumn("SEQ","项次");
		}

		private void BindRCard(System.Data.DataView dv)
		{
//			this.gridRCard.DataSource = null;
//			this.gridRCard.DataSource = dv;
//			
//			this.gridRCard.DisplayLayout.Bands[0].Columns["CollectType"].Hidden = true;
//			this.gridRCard.DisplayLayout.Bands[0].Columns["SEQ"].Hidden = true;
		}

		private void BindGrid()
		{
			this._tmpTable.AcceptChanges();
			ultraGridContent.DataSource = null;
			DataView dv = _tmpTable.DefaultView;
			dv.Sort = "ModelCode,ItemCode";
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
		#endregion 

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FUnNormalShip));
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            this.ucLEInput = new UserControl.UCLabelEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUnComplete = new UserControl.UCButton();
            this.ucBtnExit = new UserControl.UCButton();
            this.ucBtnComplete = new UserControl.UCButton();
            this.btnDeleteDetail = new UserControl.UCButton();
            this.btnAdd = new UserControl.UCButton();
            this.btnDeleteRCard = new UserControl.UCButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOutputFIFO = new UserControl.UCButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ultraOsType = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.txtCartonNum = new UserControl.UCLabelEdit();
            this.ucLETicketNo = new UserControl.UCLabelEdit();
            this.txtSumNum = new UserControl.UCLabelEdit();
            this.gbxDetail = new System.Windows.Forms.GroupBox();
            this.ultraGridContent = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel4 = new System.Windows.Forms.Panel();
            this.grpFIFO = new System.Windows.Forms.GroupBox();
            this.ultraGridFIFO = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.pnlAbnormal = new System.Windows.Forms.Panel();
            this.txtShipType = new UserControl.UCLabelCombox();
            this.btnCheck = new UserControl.UCButton();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCheck = new System.Windows.Forms.Label();
            this.cbFIFO = new System.Windows.Forms.CheckBox();
            this.rbDelete = new System.Windows.Forms.RadioButton();
            this.rbAdd = new System.Windows.Forms.RadioButton();
            this.txtMoCode = new UserControl.UCLabelEdit();
            this.txtPlanQty = new UserControl.UCLabelEdit();
            this.dateShip = new UserControl.UCDatetTime();
            this.txtPartner = new UserControl.UCLabelEdit();
            this.txtDesc = new UserControl.UCLabelEdit();
            this.cbxType = new UserControl.UCLabelCombox();
            this.txtItemDesc = new UserControl.UCLabelEdit();
            this.cbxItemCode = new UserControl.UCLabelCombox();
            this.cbxModel = new UserControl.UCLabelCombox();
            this.txtDay = new UserControl.UCLabelEdit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOsType)).BeginInit();
            this.gbxDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridContent)).BeginInit();
            this.panel4.SuspendLayout();
            this.grpFIFO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridFIFO)).BeginInit();
            this.pnlAbnormal.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucLEInput
            // 
            this.ucLEInput.AllowEditOnlyChecked = true;
            this.ucLEInput.Caption = "采集对象";
            this.ucLEInput.Checked = false;
            this.ucLEInput.EditType = UserControl.EditTypes.String;
            this.ucLEInput.Location = new System.Drawing.Point(12, 112);
            this.ucLEInput.MaxLength = 1000;
            this.ucLEInput.Multiline = false;
            this.ucLEInput.Name = "ucLEInput";
            this.ucLEInput.PasswordChar = '\0';
            this.ucLEInput.ReadOnly = false;
            this.ucLEInput.ShowCheckBox = false;
            this.ucLEInput.Size = new System.Drawing.Size(461, 22);
            this.ucLEInput.TabIndex = 1;
            this.ucLEInput.TabNext = false;
            this.ucLEInput.Value = "";
            this.ucLEInput.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLEInput.XAlign = 73;
            this.ucLEInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEInput_TxtboxKeyPress);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnUnComplete);
            this.panel1.Controls.Add(this.ucBtnExit);
            this.panel1.Controls.Add(this.ucBtnComplete);
            this.panel1.Controls.Add(this.btnDeleteDetail);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnDeleteRCard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 572);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(839, 38);
            this.panel1.TabIndex = 3;
            // 
            // btnUnComplete
            // 
            this.btnUnComplete.BackColor = System.Drawing.SystemColors.Control;
            this.btnUnComplete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUnComplete.BackgroundImage")));
            this.btnUnComplete.ButtonType = UserControl.ButtonTypes.None;
            this.btnUnComplete.Caption = "取消完成";
            this.btnUnComplete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnComplete.Location = new System.Drawing.Point(406, 7);
            this.btnUnComplete.Name = "btnUnComplete";
            this.btnUnComplete.Size = new System.Drawing.Size(88, 22);
            this.btnUnComplete.TabIndex = 4;
            this.btnUnComplete.Click += new System.EventHandler(this.btnUnComplete_Click);
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(500, 7);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 2;
            // 
            // ucBtnComplete
            // 
            this.ucBtnComplete.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnComplete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnComplete.BackgroundImage")));
            this.ucBtnComplete.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnComplete.Caption = "完成出货单";
            this.ucBtnComplete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnComplete.Location = new System.Drawing.Point(312, 8);
            this.ucBtnComplete.Name = "ucBtnComplete";
            this.ucBtnComplete.Size = new System.Drawing.Size(88, 22);
            this.ucBtnComplete.TabIndex = 1;
            this.ucBtnComplete.Click += new System.EventHandler(this.ucBtnComplete_Click);
            // 
            // btnDeleteDetail
            // 
            this.btnDeleteDetail.BackColor = System.Drawing.SystemColors.Control;
            this.btnDeleteDetail.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteDetail.BackgroundImage")));
            this.btnDeleteDetail.ButtonType = UserControl.ButtonTypes.None;
            this.btnDeleteDetail.Caption = "删除明细";
            this.btnDeleteDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteDetail.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDeleteDetail.Location = new System.Drawing.Point(218, 8);
            this.btnDeleteDetail.Name = "btnDeleteDetail";
            this.btnDeleteDetail.Size = new System.Drawing.Size(88, 22);
            this.btnDeleteDetail.TabIndex = 6;
            this.btnDeleteDetail.Click += new System.EventHandler(this.btnDeleteDetail_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.Control;
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.ButtonType = UserControl.ButtonTypes.Add;
            this.btnAdd.Caption = "新增";
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Location = new System.Drawing.Point(3, 7);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 22);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDeleteRCard
            // 
            this.btnDeleteRCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteRCard.BackColor = System.Drawing.SystemColors.Control;
            this.btnDeleteRCard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteRCard.BackgroundImage")));
            this.btnDeleteRCard.ButtonType = UserControl.ButtonTypes.None;
            this.btnDeleteRCard.Caption = "删除";
            this.btnDeleteRCard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteRCard.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDeleteRCard.Location = new System.Drawing.Point(97, 7);
            this.btnDeleteRCard.Name = "btnDeleteRCard";
            this.btnDeleteRCard.Size = new System.Drawing.Size(88, 22);
            this.btnDeleteRCard.TabIndex = 5;
            this.btnDeleteRCard.Visible = false;
            this.btnDeleteRCard.Click += new System.EventHandler(this.btnDeleteRCard_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnOutputFIFO);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.txtCartonNum);
            this.panel2.Controls.Add(this.ucLETicketNo);
            this.panel2.Controls.Add(this.txtSumNum);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(839, 74);
            this.panel2.TabIndex = 0;
            // 
            // btnOutputFIFO
            // 
            this.btnOutputFIFO.BackColor = System.Drawing.SystemColors.Control;
            this.btnOutputFIFO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOutputFIFO.BackgroundImage")));
            this.btnOutputFIFO.ButtonType = UserControl.ButtonTypes.None;
            this.btnOutputFIFO.Caption = "先进先出导出";
            this.btnOutputFIFO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOutputFIFO.Location = new System.Drawing.Point(520, 44);
            this.btnOutputFIFO.Name = "btnOutputFIFO";
            this.btnOutputFIFO.Size = new System.Drawing.Size(88, 22);
            this.btnOutputFIFO.TabIndex = 34;
            this.btnOutputFIFO.Click += new System.EventHandler(this.btnOutputFIFO_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ultraOsType);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(835, 37);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "采集方式";
            // 
            // ultraOsType
            // 
            this.ultraOsType.BackColor = System.Drawing.SystemColors.Control;
            this.ultraOsType.BackColorInternal = System.Drawing.SystemColors.Control;
            this.ultraOsType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraOsType.ImageTransparentColor = System.Drawing.Color.Gainsboro;
            valueListItem1.DisplayText = "Carton  ";
            valueListItem2.DisplayText = "PCS";
            this.ultraOsType.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2});
            this.ultraOsType.Location = new System.Drawing.Point(3, 16);
            this.ultraOsType.Name = "ultraOsType";
            this.ultraOsType.Size = new System.Drawing.Size(829, 18);
            this.ultraOsType.TabIndex = 0;
            this.ultraOsType.TabStop = false;
            this.ultraOsType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // txtCartonNum
            // 
            this.txtCartonNum.AllowEditOnlyChecked = true;
            this.txtCartonNum.Caption = "Carton数";
            this.txtCartonNum.Checked = false;
            this.txtCartonNum.EditType = UserControl.EditTypes.String;
            this.txtCartonNum.Location = new System.Drawing.Point(397, 44);
            this.txtCartonNum.MaxLength = 40;
            this.txtCartonNum.Multiline = false;
            this.txtCartonNum.Name = "txtCartonNum";
            this.txtCartonNum.PasswordChar = '\0';
            this.txtCartonNum.ReadOnly = true;
            this.txtCartonNum.ShowCheckBox = false;
            this.txtCartonNum.Size = new System.Drawing.Size(111, 22);
            this.txtCartonNum.TabIndex = 11;
            this.txtCartonNum.TabNext = false;
            this.txtCartonNum.TabStop = false;
            this.txtCartonNum.Value = "0";
            this.txtCartonNum.WidthType = UserControl.WidthTypes.Tiny;
            this.txtCartonNum.XAlign = 458;
            // 
            // ucLETicketNo
            // 
            this.ucLETicketNo.AllowEditOnlyChecked = true;
            this.ucLETicketNo.Caption = "出货单号";
            this.ucLETicketNo.Checked = false;
            this.ucLETicketNo.EditType = UserControl.EditTypes.String;
            this.ucLETicketNo.Location = new System.Drawing.Point(10, 45);
            this.ucLETicketNo.MaxLength = 40;
            this.ucLETicketNo.Multiline = false;
            this.ucLETicketNo.Name = "ucLETicketNo";
            this.ucLETicketNo.PasswordChar = '\0';
            this.ucLETicketNo.ReadOnly = false;
            this.ucLETicketNo.ShowCheckBox = false;
            this.ucLETicketNo.Size = new System.Drawing.Size(261, 22);
            this.ucLETicketNo.TabIndex = 0;
            this.ucLETicketNo.TabNext = false;
            this.ucLETicketNo.Value = "";
            this.ucLETicketNo.WidthType = UserControl.WidthTypes.Long;
            this.ucLETicketNo.XAlign = 71;
            this.ucLETicketNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLETicketNo_TxtboxKeyPress);
            // 
            // txtSumNum
            // 
            this.txtSumNum.AllowEditOnlyChecked = true;
            this.txtSumNum.Caption = "合计数量";
            this.txtSumNum.Checked = false;
            this.txtSumNum.EditType = UserControl.EditTypes.String;
            this.txtSumNum.Location = new System.Drawing.Point(280, 45);
            this.txtSumNum.MaxLength = 40;
            this.txtSumNum.Multiline = false;
            this.txtSumNum.Name = "txtSumNum";
            this.txtSumNum.PasswordChar = '\0';
            this.txtSumNum.ReadOnly = true;
            this.txtSumNum.ShowCheckBox = false;
            this.txtSumNum.Size = new System.Drawing.Size(111, 22);
            this.txtSumNum.TabIndex = 1;
            this.txtSumNum.TabNext = false;
            this.txtSumNum.Value = "0";
            this.txtSumNum.WidthType = UserControl.WidthTypes.Tiny;
            this.txtSumNum.XAlign = 341;
            // 
            // gbxDetail
            // 
            this.gbxDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxDetail.Controls.Add(this.ultraGridContent);
            this.gbxDetail.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbxDetail.Location = new System.Drawing.Point(20, 89);
            this.gbxDetail.Name = "gbxDetail";
            this.gbxDetail.Size = new System.Drawing.Size(593, 145);
            this.gbxDetail.TabIndex = 2;
            this.gbxDetail.TabStop = false;
            this.gbxDetail.Text = "出货明细";
            // 
            // ultraGridContent
            // 
            this.ultraGridContent.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridContent.Location = new System.Drawing.Point(3, 17);
            this.ultraGridContent.Name = "ultraGridContent";
            this.ultraGridContent.Size = new System.Drawing.Size(587, 125);
            this.ultraGridContent.TabIndex = 6;
            this.ultraGridContent.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridContent_InitializeLayout);
            this.ultraGridContent.AfterRowActivate += new System.EventHandler(this.ultraGridContent_AfterRowActivate);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.grpFIFO);
            this.panel4.Controls.Add(this.gbxDetail);
            this.panel4.Location = new System.Drawing.Point(-5, 193);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(592, 238);
            this.panel4.TabIndex = 5;
            // 
            // grpFIFO
            // 
            this.grpFIFO.Controls.Add(this.ultraGridFIFO);
            this.grpFIFO.Location = new System.Drawing.Point(27, 15);
            this.grpFIFO.Name = "grpFIFO";
            this.grpFIFO.Size = new System.Drawing.Size(553, 74);
            this.grpFIFO.TabIndex = 9;
            this.grpFIFO.TabStop = false;
            this.grpFIFO.Text = "先进先出检查";
            this.grpFIFO.Visible = false;
            // 
            // ultraGridFIFO
            // 
            this.ultraGridFIFO.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridFIFO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridFIFO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridFIFO.Location = new System.Drawing.Point(3, 16);
            this.ultraGridFIFO.Name = "ultraGridFIFO";
            this.ultraGridFIFO.Size = new System.Drawing.Size(547, 55);
            this.ultraGridFIFO.TabIndex = 7;
            this.ultraGridFIFO.TabStop = false;
            this.ultraGridFIFO.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridFIFO_InitializeLayout);
            // 
            // pnlAbnormal
            // 
            this.pnlAbnormal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAbnormal.Controls.Add(this.txtShipType);
            this.pnlAbnormal.Controls.Add(this.btnCheck);
            this.pnlAbnormal.Controls.Add(this.label3);
            this.pnlAbnormal.Controls.Add(this.lblCheck);
            this.pnlAbnormal.Controls.Add(this.cbFIFO);
            this.pnlAbnormal.Controls.Add(this.rbDelete);
            this.pnlAbnormal.Controls.Add(this.rbAdd);
            this.pnlAbnormal.Controls.Add(this.txtMoCode);
            this.pnlAbnormal.Controls.Add(this.txtPlanQty);
            this.pnlAbnormal.Controls.Add(this.dateShip);
            this.pnlAbnormal.Controls.Add(this.txtPartner);
            this.pnlAbnormal.Controls.Add(this.txtDesc);
            this.pnlAbnormal.Controls.Add(this.cbxType);
            this.pnlAbnormal.Controls.Add(this.txtItemDesc);
            this.pnlAbnormal.Controls.Add(this.cbxItemCode);
            this.pnlAbnormal.Controls.Add(this.cbxModel);
            this.pnlAbnormal.Controls.Add(this.ucLEInput);
            this.pnlAbnormal.Controls.Add(this.txtDay);
            this.pnlAbnormal.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAbnormal.Location = new System.Drawing.Point(0, 74);
            this.pnlAbnormal.Name = "pnlAbnormal";
            this.pnlAbnormal.Size = new System.Drawing.Size(839, 141);
            this.pnlAbnormal.TabIndex = 4;
            // 
            // txtShipType
            // 
            this.txtShipType.AllowEditOnlyChecked = true;
            this.txtShipType.Caption = "正 常 品";
            this.txtShipType.Checked = false;
            this.txtShipType.Location = new System.Drawing.Point(215, 88);
            this.txtShipType.Name = "txtShipType";
            this.txtShipType.SelectedIndex = -1;
            this.txtShipType.ShowCheckBox = false;
            this.txtShipType.Size = new System.Drawing.Size(199, 20);
            this.txtShipType.TabIndex = 32;
            this.txtShipType.WidthType = UserControl.WidthTypes.Normal;
            this.txtShipType.XAlign = 281;
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.SystemColors.Control;
            this.btnCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCheck.BackgroundImage")));
            this.btnCheck.ButtonType = UserControl.ButtonTypes.None;
            this.btnCheck.Caption = "检查";
            this.btnCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheck.Location = new System.Drawing.Point(484, 112);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(88, 22);
            this.btnCheck.TabIndex = 29;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(591, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 21);
            this.label3.TabIndex = 26;
            this.label3.Text = "天前入库的资料";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCheck
            // 
            this.lblCheck.Location = new System.Drawing.Point(497, 88);
            this.lblCheck.Name = "lblCheck";
            this.lblCheck.Size = new System.Drawing.Size(36, 21);
            this.lblCheck.TabIndex = 24;
            this.lblCheck.Text = "检查";
            this.lblCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbFIFO
            // 
            this.cbFIFO.Checked = true;
            this.cbFIFO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFIFO.Location = new System.Drawing.Point(423, 87);
            this.cbFIFO.Name = "cbFIFO";
            this.cbFIFO.Size = new System.Drawing.Size(85, 22);
            this.cbFIFO.TabIndex = 23;
            this.cbFIFO.Text = "先进先出";
            this.cbFIFO.CheckedChanged += new System.EventHandler(this.cbFIFO_CheckedChanged);
            // 
            // rbDelete
            // 
            this.rbDelete.Location = new System.Drawing.Point(640, 112);
            this.rbDelete.Name = "rbDelete";
            this.rbDelete.Size = new System.Drawing.Size(53, 22);
            this.rbDelete.TabIndex = 21;
            this.rbDelete.Tag = "ActionType";
            this.rbDelete.Text = "删除";
            // 
            // rbAdd
            // 
            this.rbAdd.Checked = true;
            this.rbAdd.Location = new System.Drawing.Point(585, 112);
            this.rbAdd.Name = "rbAdd";
            this.rbAdd.Size = new System.Drawing.Size(60, 22);
            this.rbAdd.TabIndex = 20;
            this.rbAdd.TabStop = true;
            this.rbAdd.Tag = "ActionType";
            this.rbAdd.Text = "新增";
            // 
            // txtMoCode
            // 
            this.txtMoCode.AllowEditOnlyChecked = true;
            this.txtMoCode.Caption = "返工工单";
            this.txtMoCode.Checked = false;
            this.txtMoCode.EditType = UserControl.EditTypes.String;
            this.txtMoCode.Location = new System.Drawing.Point(12, 89);
            this.txtMoCode.MaxLength = 40;
            this.txtMoCode.Multiline = false;
            this.txtMoCode.Name = "txtMoCode";
            this.txtMoCode.PasswordChar = '\0';
            this.txtMoCode.ReadOnly = false;
            this.txtMoCode.ShowCheckBox = false;
            this.txtMoCode.Size = new System.Drawing.Size(194, 22);
            this.txtMoCode.TabIndex = 19;
            this.txtMoCode.TabNext = false;
            this.txtMoCode.Value = "";
            this.txtMoCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtMoCode.XAlign = 73;
            // 
            // txtPlanQty
            // 
            this.txtPlanQty.AllowEditOnlyChecked = true;
            this.txtPlanQty.Caption = "计划数量";
            this.txtPlanQty.Checked = false;
            this.txtPlanQty.EditType = UserControl.EditTypes.Integer;
            this.txtPlanQty.Location = new System.Drawing.Point(423, 59);
            this.txtPlanQty.MaxLength = 40;
            this.txtPlanQty.Multiline = false;
            this.txtPlanQty.Name = "txtPlanQty";
            this.txtPlanQty.PasswordChar = '\0';
            this.txtPlanQty.ReadOnly = false;
            this.txtPlanQty.ShowCheckBox = false;
            this.txtPlanQty.Size = new System.Drawing.Size(194, 23);
            this.txtPlanQty.TabIndex = 17;
            this.txtPlanQty.TabNext = true;
            this.txtPlanQty.Value = "";
            this.txtPlanQty.WidthType = UserControl.WidthTypes.Normal;
            this.txtPlanQty.XAlign = 484;
            // 
            // dateShip
            // 
            this.dateShip.Caption = "发货日期";
            this.dateShip.Location = new System.Drawing.Point(218, 59);
            this.dateShip.Name = "dateShip";
            this.dateShip.ShowType = UserControl.DateTimeTypes.Date;
            this.dateShip.Size = new System.Drawing.Size(160, 20);
            this.dateShip.TabIndex = 16;
            this.dateShip.Value = new System.DateTime(2005, 10, 21, 9, 38, 5, 0);
            this.dateShip.XAlign = 281;
            // 
            // txtPartner
            // 
            this.txtPartner.AllowEditOnlyChecked = true;
            this.txtPartner.Caption = "提货方";
            this.txtPartner.Checked = false;
            this.txtPartner.EditType = UserControl.EditTypes.String;
            this.txtPartner.Location = new System.Drawing.Point(24, 59);
            this.txtPartner.MaxLength = 40;
            this.txtPartner.Multiline = false;
            this.txtPartner.Name = "txtPartner";
            this.txtPartner.PasswordChar = '\0';
            this.txtPartner.ReadOnly = false;
            this.txtPartner.ShowCheckBox = false;
            this.txtPartner.Size = new System.Drawing.Size(182, 23);
            this.txtPartner.TabIndex = 15;
            this.txtPartner.TabNext = true;
            this.txtPartner.Value = "";
            this.txtPartner.WidthType = UserControl.WidthTypes.Normal;
            this.txtPartner.XAlign = 73;
            // 
            // txtDesc
            // 
            this.txtDesc.AllowEditOnlyChecked = true;
            this.txtDesc.Caption = "备注";
            this.txtDesc.Checked = false;
            this.txtDesc.EditType = UserControl.EditTypes.String;
            this.txtDesc.Location = new System.Drawing.Point(218, 7);
            this.txtDesc.MaxLength = 40;
            this.txtDesc.Multiline = false;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.PasswordChar = '\0';
            this.txtDesc.ReadOnly = false;
            this.txtDesc.ShowCheckBox = false;
            this.txtDesc.Size = new System.Drawing.Size(463, 23);
            this.txtDesc.TabIndex = 14;
            this.txtDesc.TabNext = true;
            this.txtDesc.Value = "";
            this.txtDesc.WidthType = UserControl.WidthTypes.TooLong;
            this.txtDesc.XAlign = 281;
            // 
            // cbxType
            // 
            this.cbxType.AllowEditOnlyChecked = true;
            this.cbxType.Caption = "出货类型";
            this.cbxType.Checked = false;
            this.cbxType.Location = new System.Drawing.Point(10, 7);
            this.cbxType.Name = "cbxType";
            this.cbxType.SelectedIndex = -1;
            this.cbxType.ShowCheckBox = false;
            this.cbxType.Size = new System.Drawing.Size(196, 20);
            this.cbxType.TabIndex = 13;
            this.cbxType.WidthType = UserControl.WidthTypes.Normal;
            this.cbxType.XAlign = 73;
            // 
            // txtItemDesc
            // 
            this.txtItemDesc.AllowEditOnlyChecked = true;
            this.txtItemDesc.Caption = "产品描述";
            this.txtItemDesc.Checked = false;
            this.txtItemDesc.EditType = UserControl.EditTypes.String;
            this.txtItemDesc.Location = new System.Drawing.Point(423, 31);
            this.txtItemDesc.MaxLength = 40;
            this.txtItemDesc.Multiline = false;
            this.txtItemDesc.Name = "txtItemDesc";
            this.txtItemDesc.PasswordChar = '\0';
            this.txtItemDesc.ReadOnly = false;
            this.txtItemDesc.ShowCheckBox = false;
            this.txtItemDesc.Size = new System.Drawing.Size(194, 22);
            this.txtItemDesc.TabIndex = 12;
            this.txtItemDesc.TabNext = true;
            this.txtItemDesc.Value = "";
            this.txtItemDesc.WidthType = UserControl.WidthTypes.Normal;
            this.txtItemDesc.XAlign = 484;
            // 
            // cbxItemCode
            // 
            this.cbxItemCode.AllowEditOnlyChecked = true;
            this.cbxItemCode.Caption = "产品代码";
            this.cbxItemCode.Checked = false;
            this.cbxItemCode.Location = new System.Drawing.Point(218, 33);
            this.cbxItemCode.Name = "cbxItemCode";
            this.cbxItemCode.SelectedIndex = -1;
            this.cbxItemCode.ShowCheckBox = false;
            this.cbxItemCode.Size = new System.Drawing.Size(196, 20);
            this.cbxItemCode.TabIndex = 11;
            this.cbxItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.cbxItemCode.XAlign = 281;
            this.cbxItemCode.SelectedIndexChanged += new System.EventHandler(this.cbxItemCode_SelectedIndexChanged);
            // 
            // cbxModel
            // 
            this.cbxModel.AllowEditOnlyChecked = true;
            this.cbxModel.Caption = "产品别";
            this.cbxModel.Checked = false;
            this.cbxModel.Location = new System.Drawing.Point(10, 33);
            this.cbxModel.Name = "cbxModel";
            this.cbxModel.SelectedIndex = -1;
            this.cbxModel.ShowCheckBox = false;
            this.cbxModel.Size = new System.Drawing.Size(196, 20);
            this.cbxModel.TabIndex = 10;
            this.cbxModel.WidthType = UserControl.WidthTypes.Normal;
            this.cbxModel.XAlign = 73;
            this.cbxModel.SelectedIndexChanged += new System.EventHandler(this.cbxModel_SelectedIndexChanged);
            // 
            // txtDay
            // 
            this.txtDay.AllowEditOnlyChecked = true;
            this.txtDay.Caption = "";
            this.txtDay.Checked = false;
            this.txtDay.EditType = UserControl.EditTypes.Integer;
            this.txtDay.Location = new System.Drawing.Point(527, 88);
            this.txtDay.MaxLength = 40;
            this.txtDay.Multiline = false;
            this.txtDay.Name = "txtDay";
            this.txtDay.PasswordChar = '\0';
            this.txtDay.ReadOnly = false;
            this.txtDay.ShowCheckBox = false;
            this.txtDay.Size = new System.Drawing.Size(58, 22);
            this.txtDay.TabIndex = 11;
            this.txtDay.TabNext = false;
            this.txtDay.TabStop = false;
            this.txtDay.Value = "1";
            this.txtDay.WidthType = UserControl.WidthTypes.Tiny;
            this.txtDay.XAlign = 535;
            // 
            // FUnNormalShip
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(839, 610);
            this.Controls.Add(this.pnlAbnormal);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Name = "FUnNormalShip";
            this.Text = "非销售出货采集";
            this.Load += new System.EventHandler(this.FUnNormalShip_Load);
            this.SizeChanged += new System.EventHandler(this.FUnNormalShip_SizeChanged);
            this.Closed += new System.EventHandler(this.FUnNormalShip_Closed);
            this.Resize += new System.EventHandler(this.FUnNormalShip_Resize);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraOsType)).EndInit();
            this.gbxDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridContent)).EndInit();
            this.panel4.ResumeLayout(false);
            this.grpFIFO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridFIFO)).EndInit();
            this.pnlAbnormal.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region 数据添加部分
		/// <summary>
		/// 输入序列呈或者是二维条码添加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// 

		private void AddData()
		{
			if(this._shipNo != this.ucLETicketNo.InnerTextBox.Text.Trim())
			{
				this.LoadData();
			}
			
			//检查界面输入
			if(!InputValid())
				return ;

			try
			{	
				if( GetStatus() == ShipStatus.Shipped)
				{
					this.ErrorMessage("$Ship_Only_Init"); //只有初始状态才能序添加列号!
					return;
				}
				if ( this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.Planate.ToString() )
				{
					#region 二维条码添加
					string[] idList = null;
					try
					{
						BarCodeParse barParser = new BarCodeParse(this._domainDataProvider);
						idList = barParser.GetIDList( this.ucLEInput.Value.Trim() );
						//string model = barParser.GetModelCode( this.ucLEInput.Value.Trim() );
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
						AddShipRCard(id,CollectionType.Planate,string.Empty);
					}
					#endregion
				}
				else if ( this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.Carton.ToString() )
				{
					#region Carton
					BenQGuru.eMES.DataCollect.DataCollectFacade dc = new BenQGuru.eMES.DataCollect.DataCollectFacade(this.DataProvider);

					object[] idList = dc.GetSimulationFromCarton(this.ucLEInput.Value.Trim().ToUpper());
					if ( idList == null || idList.Length == 0)
					{
						this.ErrorMessage("$CS_RCard_List_Is_Empty");
						return;
					}

					this.DataProvider.BeginTransaction();

					//还个的序号添加，并判断是否已经存在
					foreach (BenQGuru.eMES.Domain.DataCollect.Simulation sim in idList )
					{
						AddShipRCard(sim.RunningCard,CollectionType.Carton,this.ucLEInput.Value.Trim().ToUpper());
					}
					#endregion
				}
				if ( this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.PCS.ToString() )
				{
					this.DataProvider.BeginTransaction();
					#region 序列号添加
					string id = this.ucLEInput.Value.Trim().ToUpper();
					id = id.Substring(0, Math.Min(40,id.Length));
						
					BenQGuru.eMES.DataCollect.DataCollectFacade dc = new BenQGuru.eMES.DataCollect.DataCollectFacade(this.DataProvider);
					BenQGuru.eMES.Domain.DataCollect.Simulation sim = dc.GetSimulation(id) as BenQGuru.eMES.Domain.DataCollect.Simulation;

					string cartoncode = string.Empty;
					if(sim != null)
						cartoncode = sim.CartonCode;

					AddShipRCard(id,CollectionType.PCS,cartoncode);
					#endregion
				}
					
				this.SucessMessage("$CS_Add_Success");
				ucLEInput.TextFocus(false, true);
				if(this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.Carton.ToString())
				{
					this.txtCartonNum.Value = (int.Parse(this.txtCartonNum.Value)+1).ToString();
				}

				this.DataProvider.CommitTransaction();

				if(cbFIFO.Checked)
				{
					btnCheck_Click(null,null);
				}
			}
			catch( Exception ex )
			{
				this.DataProvider.RollbackTransaction();
				this.ErrorMessage( ex.Message );
				this.LoadData();
				this.ucLEInput.TextFocus(false, true);
			}
			finally
			{
				this.ucLEInput.Value = "";
				this.CloseConnection();
			}
		}

		//点添加按钮
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			AddData();
		}

		//输入框敲回车
		private void ucLEInput_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar == '\r' )
			{	
				if(rbAdd.Checked)
				{
					AddData();
				}
				else if(rbDelete.Checked)
				{
					btnDeleteRCard_Click(null,null);
				}

				Application.DoEvents();
				ucLEInput.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
			}
		}

		//检查界面输入的有效性
		private bool InputValid()
		{
			if ( this.ucLETicketNo.Value.Trim() == string.Empty )
			{
				this.ErrorMessage("$Error_CS_Input_StockIn_TicketNo");
				this.ucLETicketNo.TextFocus(false, true);
				return false;
			}	

			if ( this.ucLEInput.Value.Trim() == string.Empty )
			{
				return false;
			}

			if(this.cbxType.ComboBoxData.SelectedIndex < 0 || this.cbxType.ComboBoxData.SelectedItem == null)
			{
				this.ErrorMessage("$Please_Select " + this.cbxType.Caption);
                this.cbxType.Focus();
				return false;
			}

			if(this.cbxModel.ComboBoxData.SelectedIndex < 0 || this.cbxModel.ComboBoxData.SelectedItem == null)
			{
				this.ErrorMessage("$Please_Select " + this.cbxModel.Caption);
                this.cbxModel.Focus();
				return false;
			}

			if(this.cbxItemCode.ComboBoxData.SelectedIndex < 0 || this.cbxItemCode.ComboBoxData.SelectedItem == null)
			{
				this.ErrorMessage("$Please_Select " + this.cbxItemCode.Caption);
                this.cbxItemCode.Focus();
				return false;
			}

			if(this.txtPlanQty.InnerTextBox.Text.Trim() == string.Empty)
			{
				this.ErrorMessage("$Please_Input " + this.txtPlanQty.Caption);
				this.txtPlanQty.TextFocus(false, true);
				return false;
			}
			return true;
		}

		//向数据库中添加数据
		private void AddShipRCard(string rcard,CollectionType c_type,string cartoncode)
		{
			//查询出货单是否存在，没有，则添加
			//object[] objs = _facade.QueryInvShip(this.ucLETicketNo.InnerTextBox.Text.Trim(),
			//									this.cbxItemCode.ComboBoxData.SelectedItem.ToString());

			InvShip ship = GetShip(this._shipNo,
									this.cbxItemCode.ComboBoxData.SelectedItem.ToString());

			//Laws Lu,2006/11/13 uniform system collect date
			DBDateTime dbDateTime;
						
			dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

			if(ship != null)
			{
				if(ship.ShipStatus != ShipStatus.Shipping)
					throw new NoAllowAddException();

				ship.PlanQty = int.Parse(this.txtPlanQty.InnerTextBox.Text.Trim());
				ship.ActQty += 1;
				if(ship.ActQty > ship.PlanQty)
					throw new GreaterException();

//				string sql = string.Format("update tblinvship set actqty=actqty + 1,planqty= {0} where shipno='{1}' and shipseq='{2}'",this.txtPlanQty.InnerTextBox.Text.Trim(),ship.ShipNo,ship.ShipSeq);
				//this.DataProvider.CustomExecute(new SQLCondition(sql));

				this._facade.IncShipQty(ship.ShipNo,ship.ShipSeq.ToString());

			}
			else //出货单不存在
			{
				ship = new InvShip();
				ship.PlanQty = int.Parse(this.txtPlanQty.InnerTextBox.Text.Trim());
				ship.ActQty = 1;
				if(ship.ActQty > ship.PlanQty)
					throw new GreaterException();

				ship.ItemCode = this.cbxItemCode.ComboBoxData.SelectedItem.ToString();
				ship.ItemDesc = this.txtItemDesc.InnerTextBox.Text;
				ship.MainitainUser = ApplicationService.Current().UserCode;
				ship.MaintainDate = dbDateTime.DBDate;
				ship.MaintainTime = dbDateTime.DBTime;
				ship.PrintDate = ship.MaintainDate;
				ship.ModelCode =this.cbxModel.ComboBoxData.SelectedItem.ToString();
				ship.PartnerCode = this.txtPartner.InnerTextBox.Text.Trim().ToUpper();

				ship.ShipDate = FormatHelper.TODateInt(this.dateShip.Value);
				ship.ShipDesc = this.txtDesc.InnerTextBox.Text.Trim();
				ship.ShipInnerType = ReceiveInnerType.Abnormal;
				ship.ShipNo = this.ucLETicketNo.InnerTextBox.Text.Trim().ToUpper();
				ship.ShipStatus = ShipStatus.Shipping;
				ship.ShipTime = 0;
				ship.ShipType = this.cbxType.ComboBoxData.SelectedItem.ToString();
				ship.ShipUser = string.Empty;
				ship.MoCode = this.txtMoCode.Value.Trim().ToUpper();
				this._facade.AddInvShip(ship);

				this.AddGridRow(ship);
			}
			
			//查询序列号是否存在，及状态是否正确
			object[] objsRCard = _facade.QueryInvRCard(rcard);
			if(objsRCard == null || objsRCard.Length <= 0)
				throw new Exception(rcard + "$$Rcard_No_Received"); //序列号还没有入库

			bool isStatusRight = false;
			InvRCard inv = null;
			foreach(object obj in objsRCard)
			{
				InvRCard testinv = obj as InvRCard;

				if(testinv.ShipNO == ship.ShipNo)
					throw new Exception(rcard + "$Rcard_Exist_In_this_Ship"); //序列号已经存在于此出货单中

				if(testinv != null && testinv.RCardStatus == RCardStatus.Received)
				{
					isStatusRight = true;
					inv = testinv; //找到一个有效的入库记录
				}
				
			}

			if(!isStatusRight)
			{
				throw new Exception(rcard + "$Rcard_Shipped"); //序列号已经出货
			}
			
			if(inv.ItemCode != ship.ItemCode)
				throw new Exception(rcard + "$Rcard_ItemCode_Error"); //序列号的产品代码和出货单不符

			bool isNormal = false;

			if(txtShipType.ComboBoxData.Text == NormalShip)
			{
				isNormal = true;
			}
			if(isNormal)
			{
				if(inv.INVRardType != INVRardType.Normal)
					throw new Exception("$ReceiveRcard_Must_Be_Normal"); //产品序列号的入库类型必须是正常库存品
			}
			else
			{
				if(inv.INVRardType != INVRardType.Unnormal)
					throw new Exception("$ReceiveRcard_Must_Be_UnNormal");//产品序列号的入库类型必须是异常库存品
			}
			// Added By Karron Qiu,2006-8-9
			//Laws lu,2006/08/28 修改
			if(cbFIFO.Checked == true)
			{
				ShipChecker checker = new ShipChecker(this.DataProvider);
				if( this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.Carton.ToString() )
				{
					checker.Check(inv,this.txtDay.Value.Trim(),isNormal,false);
				}
				else if( this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.PCS.ToString() )
				{
					checker.Check(inv,this.txtDay.Value.Trim(),isNormal,true);
				}
			}
			//End

			//所有条件都满足，更新序列号
			inv.ShipDate = dbDateTime.DBDate;
			inv.ShipTime = dbDateTime.DBTime;
			inv.ShipUser = ApplicationService.Current().UserCode;
			inv.ShipNO = ship.ShipNo;
			inv.ShipSeq = ship.ShipSeq;
			inv.RCardStatus = RCardStatus.Shipped;
			inv.ShipCollectType = c_type.ToString();
			inv.CartonCode = cartoncode;

			_facade.UpdateInvRCard(inv);

//			bool existFIFO = false;
			for(int i = 0 ; i < dtFIFO.Rows.Count ;i ++)
			{
				if(dtFIFO.Rows[i]["Rcard"].ToString().Trim() == inv.RunningCard)
				{
					dtFIFO.Rows.Remove(dtFIFO.Rows[i]);
					dtFIFO.AcceptChanges();

//					if(dtFIFO.Rows.Count == 0)
//					{
//						grpFIFO.Visible = false;
//
//						SetPosition();
//					}
					//existFIFO = true;
					break;
				}
			}

			
//			if(cbFIFO.Checked && existFIFO)
//			{
//
//			}

			

			UpdateFormData(ship,inv,FormAction.Add);

			
			//this.AddRCardGridRow(inv);

			this._currseq = ship.ShipSeq.ToString();
		}
		
		private InvShip GetShip(string no,string item)
		{
			DataRow[] drList = this._tmpTable.Select(string.Format("NO='{0}' and ItemCode='{1}'",no,item));
			if(drList != null && drList.Length > 0)
			{
				InvShip ship = new InvShip();
				ship.ShipNo = drList[0]["NO"].ToString();
				ship.ShipSeq = int.Parse(drList[0]["SEQ"].ToString());
				ship.PlanQty = int.Parse(drList[0]["PlanQty"].ToString());
				ship.ActQty = int.Parse(drList[0]["ActQty"].ToString());
				ship.ShipStatus = drList[0]["status"].ToString();
				ship.ItemCode = drList[0]["ItemCode"].ToString();

				return ship;
			}
			else
				return null;
		}
		
		private string GetStatus()
		{
			if(this._tmpTable.Rows.Count > 0)
			{
				
				return _tmpTable.Rows[0]["status"].ToString();
			}
			else
				return null;
		}

		#endregion

		#region 数据显示部分
		
		class FormAction
		{
			public static string  Add = "Add";
			public static string Del = "Del";
		}

		private void LoadData()
		{	

			if(this.ucLETicketNo.InnerTextBox.Text.Trim() == string.Empty)
				return;

			this._shipNo = this.ucLETicketNo.InnerTextBox.Text.Trim();

			txtSumNum.InnerTextBox.Text = "0";

			this._tmpTable.Rows.Clear();
			BindGrid();
			//this._tblRCard.Rows.Clear();

			//查询出库单
			InvShip ship = null;
			object[] objs = _facade.QueryInvShip(this.ucLETicketNo.InnerTextBox.Text.Trim());
			if(objs != null && objs.Length > 0)
			{
				foreach(object obj in objs)
				{
					ship = obj as InvShip;
					if(ship != null)
					{
						this.AddGridRow(ship);
					}
				}
			}

			this.txtCartonNum.Value = _facade.GetCartonShipCount(this.ucLETicketNo.InnerTextBox.Text.Trim().ToUpper()).ToString();

			if(_currseq != "0")
				this.ActivateRow(_currseq);
		}

//		private DataRow AddRCardGridRow(InvRCard inv)
//		{
//			DataRow[] drList = this._tblRCard.Select(string.Format("RCard='{0}'",inv.RunningCard));
//			if(drList == null || drList.Length ==0)
//			{
//			DataRow dr = this._tblRCard.Rows.Add(new object[]
//											{
//												inv.RunningCard,
//												inv.ShipCollectType,
//												inv.ShipSeq
//											}
//				);
//			return dr;
//			}
//			else
//				return drList[0];
//		}

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

				BindModel();

				ucLEInput.TextFocus(false, true);
			}
		}
	
		
		private void UpdateFormData(InvShip ship,InvRCard inv,string action)
		{
			if(ship == null) 
			{
				this.LoadData();
				return;
			}
	
			int n = 0;
			DataRow[] drList = this._tmpTable.Select(string.Format("NO='{0}' and SEQ={1}",ship.ShipNo,ship.ShipSeq));
			if(drList != null && drList.Length > 0)
			{
				DataRow dr = drList[0];
				n = ship.ActQty - int.Parse(dr["ActQty"].ToString());
				dr = drList[0];
				if(ship != null && ship.ActQty > 0)
					dr["ActQty"] = ship.ActQty;
				else
					dr["ActQty"] = 0;

				dr["PlanQty"] = ship.PlanQty;

				if(dr != null)
				{
					if(!ActivateRow(dr["SEQ"].ToString())) //如果当前行没有被重新激活，则将当前序列号加进到界面上
					{
//						if(inv != null)
//						{
//							if(action == FormAction.Add)
//								this.lbxRCard.Items.Add(new UIShipRCard(inv.RunningCard,inv.ShipCollectType));
//							else
//							{
//								int i = this.lbxRCard.Items.IndexOf(new UIShipRCard(inv.RunningCard,inv.ShipCollectType));
//								if(i >= 0)
//									this.lbxRCard.Items.RemoveAt(i);
//							}
//						}
//						else
//						{
//							this.ultraGridContent_AfterRowActivate(null,null);
//						}
					}

					if(dr["ActQty"].ToString() == "0")
					{
						dr.Delete();
						this.BindGrid();
					}
				}
			}
			this.txtSumNum.InnerTextBox.Text = (int.Parse(this.txtSumNum.InnerTextBox.Text)  + n).ToString();

		}

		//如果当前行不是Active的行，则重新激活,如果被重新激活，则返回true
		private bool ActivateRow(string seq)
		{
			bool ret = false;
//			foreach(Infragistics.Win.UltraWinGrid.UltraGridRow row in this.ultraGridContent.Rows)
			for ( int iGridRowLoopIndex = 0; iGridRowLoopIndex < this.ultraGridContent.Rows.Count; iGridRowLoopIndex++)
			{
				Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridContent.Rows[iGridRowLoopIndex];
				if(row.Cells["SEQ"].Text == seq)
				{
					if(!row.Activated)
					{
						row.Activated = true;
						ret = true;
					}
				}
				else
				{
					row.Activated = false;
					row.Selected = false;
				}
			}

			return ret;
		}

		private void DeleteFormData(string no,string seq)
		{
			int n = 0;
			DataRow[] drList = this._tmpTable.Select(string.Format("NO='{0}' and SEQ={1}",no,seq));
			if(drList != null && drList.Length > 0)
			{
				DataRow dr = drList[0];
				n = 0 - int.Parse(dr["ActQty"].ToString());
				
				dr.Delete();
				this.BindGrid();
			}
			this.txtSumNum.InnerTextBox.Text = (int.Parse(this.txtSumNum.InnerTextBox.Text)  + n).ToString();

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

		private DataRow AddGridRow(InvShip ship)
		{
			DataRow dr = _tmpTable.Rows.Add(new object[]
											{
												false,
												ship.ShipNo,
												ship.ShipSeq,
												ship.PartnerCode,
												FormatHelper.ToDateString(ship.ShipDate),
												ship.ModelCode,
												ship.ItemCode,
												ship.ItemDesc,
												ship.PlanQty,
												ship.ActQty,
												ship.ShipStatus
											}
								);

			txtSumNum.InnerTextBox.Text = (int.Parse(txtSumNum.InnerTextBox.Text) + ship.ActQty).ToString();

			return dr;
		}
	

		private void ultraGridContent_AfterRowActivate(object sender, System.EventArgs e)
		{
//			//this.lbxRCard.Items.Clear();
//			Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridContent.DisplayLayout.ActiveRow;
//			if(row != null)
//			{
//				string shipNo = row.Cells["NO"].Text;
//				int shipSeq = int.Parse(row.Cells["SEQ"].Text);
//
//				System.Data.DataView dv = new System.Data.DataView(this._tblRCard,"SEQ=" + shipSeq.ToString(),string.Empty,System.Data.DataViewRowState.CurrentRows);
//
//				if(dv.Count == 0)
//				{
//					object[] objsRCard = _facade.QueryInvRCard3(shipNo,shipSeq);
//					if(objsRCard != null)
//					{
//						foreach(object obj in objsRCard)
//						{
//							InvRCard inv = obj as InvRCard;
//							if(inv != null)
//							{
//								//this.lbxRCard.Items.Add(new UIShipRCard(inv.RunningCard,inv.ShipCollectType));
//								this.AddRCardGridRow(inv);
//							}	
//						}
//					}
//
//					dv = new System.Data.DataView(this._tblRCard,"SEQ=" + shipSeq.ToString(),string.Empty,System.Data.DataViewRowState.CurrentRows);
//				}
//
//				this.BindRCard(dv);
//			}
		}

		#region Bind下拉列表中的值
		protected void BindModel()
		{
			int i = this.cbxModel.ComboBoxData.SelectedIndex;
			cbxModel.ComboBoxData.Items.Clear();
			//this.cbxItemCode.ComboBoxData.Items.Clear();
			this.txtItemDesc.InnerTextBox.Text = string.Empty;

			//BenQGuru.eMES.MOModel.ModelFacade _facade = new BenQGuru.eMES.MOModel.ModelFacade(this.DataProvider);
			//object[] objs = _facade.GetAllModels();
			//只列出可入库的产品别
			object[] objs = _facade.GetInventoryModel();//DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Model), new SQLCondition(string.Format("select {0} from tblmodel where isinv='1' order by modelcode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.MOModel.Model)))));
			if(objs != null)
			{
				foreach(object obj in objs)
				{
					BenQGuru.eMES.Domain.MOModel.Model model = obj as BenQGuru.eMES.Domain.MOModel.Model;
					if(model != null)
					{
						cbxModel.ComboBoxData.Items.Add(model.ModelCode);
					}
				}
			}
			if(this.cbxModel.ComboBoxData.Items.Count > 0 && i >=0 && i<this.cbxModel.ComboBoxData.Items.Count)
			{
				this.cbxModel.ComboBoxData.SelectedIndex = i;
			}
		}

		protected void BindItemCode()
		{
			int i = this.cbxItemCode.ComboBoxData.SelectedIndex;

			string model = string.Empty;
			this.cbxItemCode.ComboBoxData.Items.Clear();
			this.txtItemDesc.InnerTextBox.Text = string.Empty;
			if(this.cbxModel.ComboBoxData.SelectedItem == null)
				return;
			else
				model = this.cbxModel.ComboBoxData.SelectedItem.ToString();

			BenQGuru.eMES.MOModel.ModelFacade _facade = new BenQGuru.eMES.MOModel.ModelFacade(this.DataProvider);
			object[] objs = _facade.GetModelAllItem(model);
			if(objs != null)
			{
				foreach(object obj in objs)
				{
					BenQGuru.eMES.Domain.MOModel.Model2Item mo = obj as BenQGuru.eMES.Domain.MOModel.Model2Item;
					if(mo != null)
					{
						this.cbxItemCode.ComboBoxData.Items.Add(mo.ItemCode);
					}
				}
			}

			if(this.cbxItemCode.ComboBoxData.Items.Count >0 && i >= 0 && i< this.cbxItemCode.ComboBoxData.Items.Count)
			{
				this.cbxItemCode.ComboBoxData.SelectedIndex = i;
			}
		}

		protected void SetItemDesc()
		{
			if(this.cbxItemCode.ComboBoxData.SelectedItem != null)
			{
				ItemFacade _facade = new ItemFacade(this.DataProvider);
                BenQGuru.eMES.Domain.MOModel.Item item = _facade.GetItem(this.cbxItemCode.ComboBoxData.SelectedItem.ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID) as BenQGuru.eMES.Domain.MOModel.Item;
				if(item != null)
				{
					this.txtItemDesc.InnerTextBox.Text = item.ItemDescription;
				}
			}
		}

		
		private void cbxModel_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.BindItemCode();
		}
		
		private void cbxItemCode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.SetItemDesc();
		}
		#endregion

		#endregion

		#region 数据删除部分
		
		//更新一个RCard中与SHIP相关的值
		private void DeleteShipInvRCard(InvRCard inv)
		{
			inv.ShipDate = 0;
			inv.ShipTime = 0;
			inv.ShipUser = string.Empty;
			inv.ShipNO = string.Empty;
			inv.ShipSeq = 0;
			inv.RCardStatus = RCardStatus.Received;
			inv.ShipCollectType = string.Empty;

			_facade.UpdateInvRCard(inv);
		}

		//删除一个序列号
		private void btnDeleteRCard_Click(object sender, System.EventArgs e)
		{
			if ( this.ucLETicketNo.Value.Trim() == string.Empty )
			{
				this.ErrorMessage("$Error_CS_Input_StockIn_TicketNo");
				this.ucLETicketNo.TextFocus(false, true);
				return;
			}	

			if ( this.ucLEInput.Value.Trim() == string.Empty )
			{
				return;
			}

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
				object[] objidList = dc.GetSimulationFromCarton(this.ucLEInput.Value.Trim().ToUpper());
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
				idList = new string[]{this.ucLEInput.Value.Trim().ToUpper()};
			}
			try
			{
				this.DataProvider.BeginTransaction();

				InvRCard inv = null;
				InvShip ship = null;
				string shipNo = this.ucLETicketNo.Value.ToUpper().Trim();

				bool isDelete = false;
				foreach(string rcard in idList)
				{
					//更新序列号
					object[] objs =_facade.QueryInvRCard4(shipNo,rcard);
					if(objs != null && objs.Length > 0)
					{
						inv = objs[0] as InvRCard;
						if(inv != null)
						{
							//判断出货单的状态
							object[] objShips = _facade.QueryInvShip(shipNo,inv.ShipSeq,1,int.MaxValue);
							if(objShips != null && objShips.Length > 0)
							{
								ship = objShips[0] as InvShip;
								if(ship != null)
								{
									if(ship.ShipStatus != ShipStatus.Shipping)
										throw new NoAllowDeleteException();

									ship.ActQty -= 1;
									if(ship.ActQty < 0)
										ship.ActQty = 0;

					
									if(ship.ActQty == 0) //如果实际数量为０，则将出货明细删除
										_facade.DeleteInvShip(ship);
									else
										_facade.UpdateInvShip(ship);
								}
							}

							if(inv.ShipCollectType != this.ultraOsType.CheckedItem.DataValue.ToString())
								throw new Exception("$INV_SHIP_COLLECT_TYPE_ERROR");

							//更新序列号
							inv.ShipDate = 0;
							inv.ShipTime = 0;
							inv.ShipUser = string.Empty;
							inv.ShipNO = string.Empty;
							inv.ShipSeq = 0;
							inv.RCardStatus = RCardStatus.Received;
							inv.ShipCollectType = string.Empty;

							_facade.UpdateInvRCard(inv);

							this.UpdateFormData(ship,inv,FormAction.Del);
							isDelete = true;
						}
					}
				}

				this.ultraGridContent_AfterRowActivate(null,null);

				this.ucLEInput.Value = string.Empty;
				if(isDelete)
				{
					if(this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.Carton.ToString())
					{
						this.txtCartonNum.Value = (int.Parse(this.txtCartonNum.Value)-1).ToString();
					}
					this.SucessMessage("$CS_Delete_Success");
				}
				this.DataProvider.CommitTransaction();

				if(cbFIFO.Checked)
				{
					btnCheck_Click(sender,e);
				}
			}
			catch(System.Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				this.ErrorMessage(ex.Message);
				this.LoadData();
			}
			finally
			{
				this.ucLEInput.Value = string.Empty;
				this.CloseConnection();
			}
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
					InvShip ship = null;
					if(row.Cells["*"].Text.ToLower() == "true")
					{
						string shipNo = row.Cells["NO"].Text;
						int seq = int.Parse(row.Cells["SEQ"].Text);
						this._currseq = "0";

			
						object[] objShips = _facade.QueryInvShip(shipNo,seq,1,int.MaxValue);
						if(objShips != null && objShips.Length > 0)
						{
							ship = objShips[0] as InvShip;
							if(ship != null)
							{
								//判断出货单的状态
								if(ship.ShipStatus != ShipStatus.Shipping)
									throw new NoAllowDeleteException();
								
								object[] objs =_facade.QueryInvRCard3(shipNo,seq);
								if(objs != null && objs.Length > 0)
								{
									foreach(object obj in objs)
									{
										InvRCard inv = obj as InvRCard;
										if(inv != null)
										{
											DeleteShipInvRCard(inv);
										}
									}
								}
								//删除出货单
								_facade.DeleteInvShip(ship);	
							}
						}
						isDeleted = true;
					}
				}
				this.DataProvider.CommitTransaction();
				if(isDeleted)
				{
					this.LoadData();
					this.txtCartonNum.Value = _facade.GetCartonShipCount(this.ucLETicketNo.Text.Trim().ToUpper()).ToString();
					this.SucessMessage("$CS_Delete_Success");
				}
			}
			catch(System.Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				this.ErrorMessage(ex.Message);
				this.LoadData();
			}
			finally
			{
				this.CloseConnection();
			}
		}
		
		#endregion

		#region 执行完成出货的动作
		private void ucBtnComplete_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.DataProvider.BeginTransaction();

				_facade.CompleteInvShip(this.ucLETicketNo.InnerTextBox.Text.Trim(),
					ApplicationService.Current().UserCode);	

				foreach(DataRow dr in this._tmpTable.Rows)
				{
					dr["status"] = ShipStatus.Shipped;
				}

				this.DataProvider.CommitTransaction();

				this.SucessMessage("$Ship_Completed");//完成出货单成功
			}
			catch(System.Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				this.ErrorMessage(ex.Message);
				this.LoadData();
			}
			finally
			{
				this.CloseConnection();
			}
		}
		
		//取消完成出货单
		private void btnUnComplete_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.DataProvider.BeginTransaction();

				_facade.UnCompleteInvShip(this.ucLETicketNo.InnerTextBox.Text.Trim(),
					ApplicationService.Current().UserCode);	

				this.DataProvider.CommitTransaction();

				foreach(DataRow dr in this._tmpTable.Rows)
				{
					dr["status"] = ShipStatus.Shipping;
				}

				this.SucessMessage("$Ship_UnCompleted"); //取消完成成功
			}
			catch(System.Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				this.ErrorMessage(ex.Message);
				this.LoadData();
			}
			finally
			{
				this.CloseConnection();
			}
		}

		#endregion		

		private void cbFIFO_CheckedChanged(object sender, System.EventArgs e)
		{
			this.txtDay.Enabled = this.cbFIFO.Checked;
		}

		#region Laws Lu,2006/08/25	FIFO

		private void SetPosition()
		{
			panel4.Top = panel2.Height + pnlAbnormal.Height;
			panel4.Left = 0;
			panel4.Height = this.Height - panel1.Height - panel2.Height;
			panel4.Width = this.Width;

			grpFIFO.Left = 0;
			grpFIFO.Top = 0;//panel4.Top ;
			grpFIFO.Width = panel4.Width;

			grpFIFO.Height = Convert.ToInt32(Decimal.Truncate(Convert.ToDecimal(panel4.Height * 0.2)));

			if(grpFIFO.Visible == true)
			{
				gbxDetail.Top = grpFIFO.Height;
				gbxDetail.Height = panel4.Height - grpFIFO.Height;
			}
			else
			{
				gbxDetail.Top = 0;
				gbxDetail.Height = panel4.Height;
			}

			gbxDetail.Left = 0;
			gbxDetail.Width = panel4.Width;
		}

		private void FUnNormalShip_Resize(object sender, System.EventArgs e)
		{
			SetPosition();
		}

		private void FUnNormalShip_SizeChanged(object sender, System.EventArgs e)
		{
			SetPosition();
		}

		private object[] LoadFIFODataSource()
		{
			object[] objs = null;
			int d = Web.Helper.FormatHelper.TODateInt(DateTime.Today.AddDays(-Convert.ToInt32(txtDay.Value)));

			//			if( this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.Carton.ToString() )
			//			{
			objs = (new InventoryFacade(DataProvider)).QueryFIFOInvRCardByItem(this.cbxItemCode.ComboBoxData.Text.ToUpper().Trim(),d);
			//			}
			//			else
			//			{
			//				objs = (new InventoryFacade(DataProvider)).QueryFIFOInvRCardByItem(ucLEInput.Value.Trim(),cbxItemCode.ComboBoxData.Text.ToUpper().Trim(),d);
			//			}
			
			return objs;
		}

		private void FillFIFOGrid(object[] objs)
		{
			dtFIFO.Clear();

			if(objs != null)
			{
				foreach(object obj in objs)
				{
					if(obj != null)
					{
						InvRCard invRcard = obj as InvRCard;


						dtFIFO.Rows.Add(new object[]{
														invRcard.CartonCode
														,invRcard.RunningCard
														,invRcard.MOCode
														,invRcard.ItemCode
														,invRcard.ReceiveDate
														,invRcard.ReceiveTime
													});
					}
				}

				dtFIFO.AcceptChanges();

				ultraGridFIFO.DataSource = dtFIFO;

				if(dtFIFO.Rows.Count > 0)
				{
					grpFIFO.Visible = true;

					SetPosition();
				}
			}
		}


		private void btnCheck_Click(object sender, System.EventArgs e)
		{
			if(cbFIFO.Checked == true)
			{
				//			if(rbAdd.Checked == true)
				//			{
				FillFIFOGrid(LoadFIFODataSource());
				//			}
			}
			else
			{
				dtFIFO.Clear();
				dtFIFO.AcceptChanges();
				grpFIFO.Visible = false;
				SetPosition();
			}
		}

		private void ultraGridFIFO_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
		{
			UltraWinGridHelper gridHelper = new UltraWinGridHelper(this.ultraGridFIFO);
			gridHelper.AddCommonColumn("CartonCode","Carton箱号");
			gridHelper.AddCommonColumn("Rcard","产品序列号");
			gridHelper.AddCommonColumn("ModelCode","产品别");
			gridHelper.AddCommonColumn("ItemCode","产品代码");
			gridHelper.AddCommonColumn("RecDate","入库日期");
			gridHelper.AddCommonColumn("RecTime","入库时间");
		}

		#endregion

		private void btnOutputFIFO_Click(object sender, System.EventArgs e)
		{
			FileDialog fd = new SaveFileDialog();
			fd.AddExtension = true;
			//fd.
			fd.Filter = "CSV数据文件|*.CSV";

			DialogResult diaResult =  fd.ShowDialog();

			string fileName = fd.FileName;

			if(diaResult == DialogResult.OK &&  fileName != String.Empty)
			{
				System.IO.FileStream fs =  System.IO.File.Create(fileName);
				try
				{
					string colName = String.Empty ;

					foreach(DataColumn dc in dtFIFO.Columns)
					{
						colName += dc.ColumnName + ",";
					}

					colName = colName.Substring(0,colName.Length - 1);
					colName += "\r\n";

					byte[] btCol = System.Text.Encoding.Default.GetBytes(colName);
					fs.Write(btCol,0,btCol.Length);

					foreach(DataRow dr in dtFIFO.Rows)
					{
						string rowData = String.Empty ;

						rowData = dr["CartonCode"].ToString() + ","
							+ dr["Rcard"].ToString() + ","
							+ dr["ModelCode"].ToString() + ","
							+ dr["ItemCode"].ToString() + ","
							+ dr["RecDate"].ToString() + ","
							+ dr["RecTime"].ToString();
						rowData = rowData + "\r\n";
				
						byte[] bt = System.Text.Encoding.Default.GetBytes(rowData);
						fs.Write(bt,0,bt.Length);
					}
				}
				catch(Exception ex)
				{
					Log.Error(ex.Message);
				}
				finally
				{
					fs.Flush();
					fs.Close();
				}
			}
		}


	}
}
