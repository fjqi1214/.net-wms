#region using
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
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using UserControl;
using BenQGuru.eMES.OQC;
#endregion

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// 入库单资料维护
	/// joe song 20050119
	/// </summary>
	public class FInvReceive : System.Windows.Forms.Form
	{
		#region 变量声明部分
		private System.ComponentModel.Container components = null;

		#endregion

		private UserControl.UCLabelCombox cbxType;
		private UserControl.UCLabelEdit txtDesc;
		private UserControl.UCLabelCombox cbxModel;
		private UserControl.UCLabelCombox cbxItemCode;
		private UserControl.UCLabelEdit txtItemDesc;
		private DataTable _tmpTable;
		private UserControl.UCButton btnQuit;
		private UserControl.UCButton btnAdd;
		private UserControl.UCButton btnDelete;
		private System.Windows.Forms.GroupBox grxDetail;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridContent;
		private System.Windows.Forms.Panel panel1;
		private string _nowRecNo;
		private FInfoForm _infoForm;
		private System.Windows.Forms.GroupBox grbDataInput;
		private System.Windows.Forms.TextBox txtNum;
		private System.Windows.Forms.Label lblNum;
		private System.Windows.Forms.TextBox txtRecNo;
		private System.Windows.Forms.Label lblRecNo;
		private System.Windows.Forms.TextBox txtMoCode;
		private System.Windows.Forms.Label lblMo;

		#region 系统功能和初始化代码
		private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
		public FInvReceive()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		
		}

		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FInvReceive));
            this.cbxType = new UserControl.UCLabelCombox();
            this.txtDesc = new UserControl.UCLabelEdit();
            this.cbxModel = new UserControl.UCLabelCombox();
            this.cbxItemCode = new UserControl.UCLabelCombox();
            this.txtItemDesc = new UserControl.UCLabelEdit();
            this.btnQuit = new UserControl.UCButton();
            this.btnAdd = new UserControl.UCButton();
            this.btnDelete = new UserControl.UCButton();
            this.grxDetail = new System.Windows.Forms.GroupBox();
            this.ultraGridContent = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grbDataInput = new System.Windows.Forms.GroupBox();
            this.lblMo = new System.Windows.Forms.Label();
            this.txtMoCode = new System.Windows.Forms.TextBox();
            this.lblRecNo = new System.Windows.Forms.Label();
            this.txtRecNo = new System.Windows.Forms.TextBox();
            this.lblNum = new System.Windows.Forms.Label();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.grxDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridContent)).BeginInit();
            this.panel1.SuspendLayout();
            this.grbDataInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxType
            // 
            this.cbxType.AllowEditOnlyChecked = true;
            this.cbxType.Caption = "类型";
            this.cbxType.Checked = false;
            this.cbxType.Location = new System.Drawing.Point(262, 19);
            this.cbxType.Name = "cbxType";
            this.cbxType.SelectedIndex = -1;
            this.cbxType.ShowCheckBox = false;
            this.cbxType.Size = new System.Drawing.Size(160, 24);
            this.cbxType.TabIndex = 1;
            this.cbxType.WidthType = UserControl.WidthTypes.Normal;
            this.cbxType.XAlign = 310;
            // 
            // txtDesc
            // 
            this.txtDesc.AllowEditOnlyChecked = true;
            this.txtDesc.Caption = "备注";
            this.txtDesc.CausesValidation = false;
            this.txtDesc.Checked = false;
            this.txtDesc.EditType = UserControl.EditTypes.String;
            this.txtDesc.Location = new System.Drawing.Point(10, 134);
            this.txtDesc.MaxLength = 40;
            this.txtDesc.Multiline = false;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.PasswordChar = '\0';
            this.txtDesc.ReadOnly = false;
            this.txtDesc.ShowCheckBox = false;
            this.txtDesc.Size = new System.Drawing.Size(364, 24);
            this.txtDesc.TabIndex = 7;
            this.txtDesc.TabNext = true;
            this.txtDesc.Value = "";
            this.txtDesc.WidthType = UserControl.WidthTypes.TooLong;
            this.txtDesc.XAlign = 41;
            // 
            // cbxModel
            // 
            this.cbxModel.AllowEditOnlyChecked = true;
            this.cbxModel.Caption = "产品别";
            this.cbxModel.Checked = false;
            this.cbxModel.Location = new System.Drawing.Point(35, 74);
            this.cbxModel.Name = "cbxModel";
            this.cbxModel.SelectedIndex = -1;
            this.cbxModel.ShowCheckBox = false;
            this.cbxModel.Size = new System.Drawing.Size(158, 20);
            this.cbxModel.TabIndex = 3;
            this.cbxModel.WidthType = UserControl.WidthTypes.Normal;
            this.cbxModel.XAlign = 82;
            this.cbxModel.SelectedIndexChanged += new System.EventHandler(this.cbxModel_SelectedIndexChanged);
            // 
            // cbxItemCode
            // 
            this.cbxItemCode.AllowEditOnlyChecked = true;
            this.cbxItemCode.Caption = "产品代码";
            this.cbxItemCode.Checked = false;
            this.cbxItemCode.Location = new System.Drawing.Point(249, 74);
            this.cbxItemCode.Name = "cbxItemCode";
            this.cbxItemCode.SelectedIndex = -1;
            this.cbxItemCode.ShowCheckBox = false;
            this.cbxItemCode.Size = new System.Drawing.Size(173, 24);
            this.cbxItemCode.TabIndex = 4;
            this.cbxItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.cbxItemCode.XAlign = 311;
            this.cbxItemCode.SelectedIndexChanged += new System.EventHandler(this.cbxItemCode_SelectedIndexChanged);
            // 
            // txtItemDesc
            // 
            this.txtItemDesc.AllowEditOnlyChecked = true;
            this.txtItemDesc.Caption = "产品描述";
            this.txtItemDesc.Checked = false;
            this.txtItemDesc.EditType = UserControl.EditTypes.String;
            this.txtItemDesc.Location = new System.Drawing.Point(23, 104);
            this.txtItemDesc.MaxLength = 40;
            this.txtItemDesc.Multiline = false;
            this.txtItemDesc.Name = "txtItemDesc";
            this.txtItemDesc.PasswordChar = '\0';
            this.txtItemDesc.ReadOnly = false;
            this.txtItemDesc.ShowCheckBox = false;
            this.txtItemDesc.Size = new System.Drawing.Size(170, 24);
            this.txtItemDesc.TabIndex = 5;
            this.txtItemDesc.TabNext = true;
            this.txtItemDesc.TabStop = false;
            this.txtItemDesc.Value = "";
            this.txtItemDesc.WidthType = UserControl.WidthTypes.Normal;
            this.txtItemDesc.XAlign = 83;
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.BackColor = System.Drawing.SystemColors.Control;
            this.btnQuit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuit.BackgroundImage")));
            this.btnQuit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnQuit.Caption = "退出";
            this.btnQuit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuit.Location = new System.Drawing.Point(708, 7);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnQuit.Size = new System.Drawing.Size(88, 22);
            this.btnQuit.TabIndex = 0;
            this.btnQuit.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.Control;
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.ButtonType = UserControl.ButtonTypes.Add;
            this.btnAdd.Caption = "新增";
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Location = new System.Drawing.Point(442, 134);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 22);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.TabStop = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.ButtonType = UserControl.ButtonTypes.Delete;
            this.btnDelete.Caption = "删除";
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Location = new System.Drawing.Point(23, 7);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(88, 22);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.TabStop = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // grxDetail
            // 
            this.grxDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grxDetail.Controls.Add(this.ultraGridContent);
            this.grxDetail.Location = new System.Drawing.Point(0, 178);
            this.grxDetail.Name = "grxDetail";
            this.grxDetail.Size = new System.Drawing.Size(808, 326);
            this.grxDetail.TabIndex = 11;
            this.grxDetail.TabStop = false;
            this.grxDetail.Text = "入库单明细";
            // 
            // ultraGridContent
            // 
            this.ultraGridContent.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridContent.Location = new System.Drawing.Point(3, 16);
            this.ultraGridContent.Name = "ultraGridContent";
            this.ultraGridContent.Size = new System.Drawing.Size(802, 307);
            this.ultraGridContent.TabIndex = 8;
            this.ultraGridContent.TabStop = false;
            this.ultraGridContent.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridContent_InitializeLayout);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnQuit);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 508);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(808, 37);
            this.panel1.TabIndex = 12;
            // 
            // grbDataInput
            // 
            this.grbDataInput.Controls.Add(this.lblMo);
            this.grbDataInput.Controls.Add(this.txtMoCode);
            this.grbDataInput.Controls.Add(this.lblRecNo);
            this.grbDataInput.Controls.Add(this.txtRecNo);
            this.grbDataInput.Controls.Add(this.lblNum);
            this.grbDataInput.Controls.Add(this.txtNum);
            this.grbDataInput.Controls.Add(this.cbxType);
            this.grbDataInput.Controls.Add(this.txtDesc);
            this.grbDataInput.Controls.Add(this.cbxModel);
            this.grbDataInput.Controls.Add(this.cbxItemCode);
            this.grbDataInput.Controls.Add(this.txtItemDesc);
            this.grbDataInput.Controls.Add(this.btnAdd);
            this.grbDataInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbDataInput.Location = new System.Drawing.Point(0, 0);
            this.grbDataInput.Name = "grbDataInput";
            this.grbDataInput.Size = new System.Drawing.Size(808, 171);
            this.grbDataInput.TabIndex = 13;
            this.grbDataInput.TabStop = false;
            this.grbDataInput.Text = "数据输入";
            // 
            // lblMo
            // 
            this.lblMo.Location = new System.Drawing.Point(36, 50);
            this.lblMo.Name = "lblMo";
            this.lblMo.Size = new System.Drawing.Size(44, 21);
            this.lblMo.TabIndex = 106;
            this.lblMo.Text = "工单";
            // 
            // txtMoCode
            // 
            this.txtMoCode.Location = new System.Drawing.Point(83, 48);
            this.txtMoCode.Name = "txtMoCode";
            this.txtMoCode.Size = new System.Drawing.Size(111, 20);
            this.txtMoCode.TabIndex = 2;
            this.txtMoCode.Leave += new System.EventHandler(this.txtMoCode_Leave);
            this.txtMoCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMoCode_KeyPress);
            this.txtMoCode.Enter += new System.EventHandler(this.txtMoCode_Enter);
            // 
            // lblRecNo
            // 
            this.lblRecNo.Location = new System.Drawing.Point(20, 22);
            this.lblRecNo.Name = "lblRecNo";
            this.lblRecNo.Size = new System.Drawing.Size(60, 22);
            this.lblRecNo.TabIndex = 104;
            this.lblRecNo.Text = "入库单号";
            // 
            // txtRecNo
            // 
            this.txtRecNo.Location = new System.Drawing.Point(83, 19);
            this.txtRecNo.Name = "txtRecNo";
            this.txtRecNo.Size = new System.Drawing.Size(111, 20);
            this.txtRecNo.TabIndex = 0;
            this.txtRecNo.Leave += new System.EventHandler(this.txtRecNo_Leave);
            this.txtRecNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRecNo_TxtboxKeyPress);
            this.txtRecNo.Enter += new System.EventHandler(this.txtRecNo_Enter);
            // 
            // lblNum
            // 
            this.lblNum.Location = new System.Drawing.Point(245, 110);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(64, 21);
            this.lblNum.TabIndex = 102;
            this.lblNum.Text = "计划数量";
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(311, 108);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(111, 20);
            this.txtNum.TabIndex = 6;
            this.txtNum.Leave += new System.EventHandler(this.txtNum_Leave);
            this.txtNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNum_TxtboxKeyPress);
            this.txtNum.Enter += new System.EventHandler(this.txtNum_Enter);
            // 
            // FInvReceive
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(808, 545);
            this.Controls.Add(this.grbDataInput);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grxDetail);
            this.Name = "FInvReceive";
            this.Text = "入库单资料维护";
            this.Load += new System.EventHandler(this.FInvReceive_Load);
            this.Closed += new System.EventHandler(this.FInvReceive_Closed);
            this.Activated += new System.EventHandler(this.FInvReceive_Activated);
            this.grxDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridContent)).EndInit();
            this.panel1.ResumeLayout(false);
            this.grbDataInput.ResumeLayout(false);
            this.grbDataInput.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void FInvReceive_Closed(object sender, System.EventArgs e)
		{
			CloseConnection();
		}

		private void CloseConnection()
		{
			if (this.DataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection(); 
		}
		private void ErrorMsg(string msg)
		{
			try
			{
				_infoForm.Add(new UserControl.Message(UserControl.MessageType.Error,msg));
			}
			catch
			{}
		}

		private void SucessMsg(string msg)
		{
			try
			{
				_infoForm.Add(new UserControl.Message(UserControl.MessageType.Success,msg));
			}
			catch
			{}
		}

		#endregion

		#region 界面显示
		private void FInvReceive_Load(object sender, System.EventArgs e)
		{
			_infoForm = ApplicationRun.GetInfoForm();

			UserControl.UIStyleBuilder.FormUI(this);
			
			this.txtRecNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;

			#region 初始化Grid
			UserControl.UIStyleBuilder.GridUI(this.ultraGridContent);
			_tmpTable = new DataTable();
			_tmpTable.Columns.Clear();
			_tmpTable.Columns.Add("*",typeof(bool));
			_tmpTable.Columns.Add( "MoCode", typeof( string ));
			_tmpTable.Columns.Add("NO",typeof(string));
			_tmpTable.Columns.Add("SEQ",typeof(int));
			_tmpTable.Columns.Add( "ModelCode", typeof( string ));
			_tmpTable.Columns.Add( "ItemCode", typeof( string ));
			_tmpTable.Columns.Add( "ItemDesc", typeof( string ));
			_tmpTable.Columns.Add( "PlayQty", typeof( string ) );
			_tmpTable.Columns.Add( "ActQty", typeof( string ) );
			_tmpTable.Columns.Add( "MOPlanQty", typeof( string ) );
			_tmpTable.Columns.Add( "MOActQty", typeof( string ) );
			
			BindGrid();
			#endregion

			//入库单类型
			this.cbxType.ComboBoxData.Items.Clear();
			object[] types= new BenQGuru.eMES.BaseSetting.SystemSettingFacade(this.DataProvider).GetParametersByParameterGroup("INVRECEIVE");
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
				if(this.cbxType.ComboBoxData.Items.Count > 0)
					this.cbxType.ComboBoxData.SelectedIndex = 0;
			}
			//产品别列表
			BindModel(this.DataProvider,this.cbxModel);
		}

		private void ultraGridContent_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
		{
			UltraWinGridHelper gridHelper = new UltraWinGridHelper(this.ultraGridContent);
			gridHelper.AddCommonColumn("*","*");
			this.ultraGridContent.DisplayLayout.Bands[0].Columns["*"].Width = 20;
			gridHelper.AddCommonColumn("MoCode","工单");
			gridHelper.AddCommonColumn("NO","NO");
			gridHelper.AddCommonColumn("SEQ","SEQ");
			gridHelper.AddCommonColumn("ModelCode","产品别");
			gridHelper.AddCommonColumn("ItemCode","产品代码");
			gridHelper.AddCommonColumn("ItemDesc","产品描述");
			gridHelper.AddCommonColumn("PlayQty","入库单计划数量");
			gridHelper.AddCommonColumn("ActQty","入库单已入库数量");
			gridHelper.AddCommonColumn("MOPlanQty","工单计划数量");
			gridHelper.AddCommonColumn("MOActQty","工单已入库数量");
		}

		private void BindGrid()
		{
			DataView dv = _tmpTable.DefaultView;
			dv.Sort = "MoCode,ModelCode,ItemCode";
			
			ultraGridContent.DataSource = dv;
			ultraGridContent.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
			ultraGridContent.DisplayLayout.Bands[0].Columns["SEQ"].Hidden = true;
			ultraGridContent.DisplayLayout.Bands[0].Columns["NO"].Hidden = true;
			for(int i=1;i<this.ultraGridContent.DisplayLayout.Bands[0].Columns.Count;i++)
			{
				this.ultraGridContent.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
			}
		}
		//Bind产品别下拉列表
		private void BindModel(BenQGuru.eMES.Common.Domain.IDomainDataProvider provider,UserControl.UCLabelCombox cbx)
		{
			cbx.ComboBoxData.Items.Clear();
			this.cbxItemCode.ComboBoxData.Items.Clear();
			this.txtItemDesc.InnerTextBox.Text = string.Empty;

			//只列出可入库的产品别
			object[] objs = new BenQGuru.eMES.Material.InventoryFacade(this.DataProvider).GetInventoryModel();
			if(objs != null)
			{
				foreach(object obj in objs)
				{
					BenQGuru.eMES.Domain.MOModel.Model model = obj as BenQGuru.eMES.Domain.MOModel.Model;
					if(model != null)
					{
						cbx.AddItem(model.ModelCode,model.ModelCode);
					}
				}
			}
		}

		//bind 料品下拉列表
		private void BindItem(BenQGuru.eMES.Common.Domain.IDomainDataProvider  provider,string model,UCLabelCombox cbx)
		{
			cbx.Clear();
			this.txtItemDesc.InnerTextBox.Text = string.Empty;
			if(provider == null || model == null || model ==string.Empty) return;

			BenQGuru.eMES.MOModel.ModelFacade _facade = new BenQGuru.eMES.MOModel.ModelFacade(provider);
			object[] objs = _facade.GetModelAllItem(model);
			if(objs != null)
			{
				foreach(object obj in objs)
				{
					BenQGuru.eMES.Domain.MOModel.Model2Item mo = obj as BenQGuru.eMES.Domain.MOModel.Model2Item;
					if(mo != null)
					{
						cbx.AddItem(mo.ItemCode,mo.ItemCode);
					}
				}
			}
		}

		
		//Bind产品列表
		private void cbxModel_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.cbxModel.ComboBoxData.SelectedItem != null)
				BindItem(this.DataProvider,this.cbxModel.ComboBoxData.SelectedItem.ToString(),this.cbxItemCode);
		}
		
		//取料器描述
		private void cbxItemCode_SelectedIndexChanged(object sender, System.EventArgs e)
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

		/// <summary>
		/// 将入库单中的记录加载到Ｇrid中
		/// </summary>
		private void LoadData()
		{
			this.txtRecNo.Text = this.txtRecNo.Text.Trim().ToUpper();
			this._nowRecNo = this.txtRecNo.Text.Trim();

			this._tmpTable.Rows.Clear();
			this._tmpTable.AcceptChanges();
			this.ultraGridContent.DataSource = null;
			BindGrid();

			InventoryFacade _facade = new InventoryFacade(this.DataProvider);
			object[] objs = _facade.QueryInvReceive(this.txtRecNo.Text.Trim().ToUpper());
			if(objs != null && objs.Length > 0)
			{
				foreach(object obj in objs)
				{
					InvReceive rec = obj as InvReceive;
					if(rec != null)
					{
						AddDataRow(rec);	
					}
				}
			}
		}

		private void AddDataRow(InvReceive rec)
		{
			InventoryFacade _facade = new InventoryFacade(this.DataProvider);

			MO mo = _facade.GetMOPlanQtyActQty(rec.MoCode);

			decimal MOPlanQty = 0;
			decimal MOActQty = 0;

			if(mo != null)
			{
				MOPlanQty = mo.MOPlanQty;
				MOActQty = mo.MOActualQty;
			}

			this._tmpTable.Rows.Add(new object[]{false,
													rec.MoCode,
													rec.RecNo,
													rec.RecSeq,
													rec.ModelCode,
													rec.ItemCode,
													rec.ItemDesc,
													rec.PlanQty,
													rec.ActQty,
													MOPlanQty,
													MOActQty
												});
			this._tmpTable.AcceptChanges();
			ActivateRow(rec.RecSeq.ToString());
			
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

		/// <summary>
		/// 根据工单把产品，产品别信息取出来
		/// </summary>
		private void txtMoCode_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				string mocode = this.txtMoCode.Text.Trim().ToUpper();
				if(mocode.Length == 0)
					return;

				BenQGuru.eMES.MOModel.MOFacade facade = new MOFacade(this.DataProvider);
				BenQGuru.eMES.Domain.MOModel.MO mo = facade.GetMO(mocode) as BenQGuru.eMES.Domain.MOModel.MO;
				if(mo == null || mo.MOCode == null)
				{
					ErrorMsg("$CS_MO_Not_Exist");
                    this.txtMoCode.Focus();
					return;
				}

				BenQGuru.eMES.MOModel.ModelFacade modelfacade = new ModelFacade(this.DataProvider);
				BenQGuru.eMES.Domain.MOModel.Model model= modelfacade.GetModelByItemCode(mo.ItemCode) as BenQGuru.eMES.Domain.MOModel.Model;

				if(model == null)
				{
					ErrorMsg("$CS_Model_Lost");
					return;
				}
				BenQGuru.eMES.MOModel.ItemFacade itemfacade = new ItemFacade(this.DataProvider);
                BenQGuru.eMES.Domain.MOModel.Item item = itemfacade.GetItem(mo.ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID) as BenQGuru.eMES.Domain.MOModel.Item;
				if(item == null)
				{
					ErrorMsg("$Error_ItemCode_NotExist");
					return;
				}

				this.cbxModel.SetSelectItem(model.ModelCode);
				this.cbxItemCode.SetSelectItem(mo.ItemCode);
				this.txtItemDesc.Value = item.ItemDescription;

                this.txtNum.Focus();
			}

		}

		#endregion

		#region 数据添加\删除部分

		private void ClearInput()
		{
			this.txtMoCode.Text = string.Empty;
			this.txtItemDesc.Value = string.Empty;
			this.cbxModel.SetSelectItem(string.Empty);
			this.cbxItemCode.SetSelectItem(string.Empty);
			this.txtNum.Text = string.Empty;
		}

		private bool InputValid()
		{
			if(this.txtRecNo.Text.Trim() == string.Empty)
			{
				this.ErrorMsg("$Error_CS_Input_StockIn_TicketNo");
                this.txtRecNo.Focus();
				return false;
			}

			//if(this.txtMoCode.Text.Trim() == string.Empty)
			//{
			//	this.ErrorMsg("$Please_Input" + this.lblMo.Text);
			//	this.txtMoCode.TextFocus(false, true);
			//	return false;
			//}

			if(this.cbxType.ComboBoxData.SelectedItem == null || this.cbxType.ComboBoxData.SelectedItem.ToString() == string.Empty)
			{
				this.ErrorMsg("$Please_Select " + cbxType.Caption);
                this.cbxType.Focus();
				return false;
			}
			
			if(this.cbxModel.ComboBoxData.SelectedItem == null || this.cbxModel.ComboBoxData.SelectedItem.ToString() == string.Empty)
			{
				this.ErrorMsg("$Please_Select " + cbxModel.Caption);
                this.cbxModel.Focus();
				return false;
			}

			if(this.cbxItemCode.ComboBoxData.SelectedItem == null || this.cbxItemCode.ComboBoxData.SelectedItem.ToString() == string.Empty)
			{
				this.ErrorMsg("$Please_Select "+this.cbxItemCode.Caption);
                this.cbxItemCode.Focus();
				return false;
			}

			if(this.txtNum.Text.Trim() == string.Empty)
			{
				this.ErrorMsg("$Please_Input "+this.lblNum.Text);
                this.txtNum.Focus();
				return false;
			}
			return true;
		}
	

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

				if(!this.InputValid()) return;

				if(this._nowRecNo != this.txtRecNo.Text)
				{
					this.LoadData();
				}

				try
				{
					InventoryFacade _facade = new InventoryFacade(this.DataProvider);

					//检查状态，初始状态才能添加
					string status = _facade.GetInvReceiveStatus(this.txtRecNo.Text.Trim());
					if(status != null && status != ReceiveStatus.Receiving)
					{
						ErrorMsg("$CS_Inv_Rec_Init_Only");//只有初始状态的入库单状态才可以添加明细
						return;
					}
					//检查是不已经存在
					
					if(_facade.InvReceiveExistByMoItem(this.txtRecNo.Text.Trim(),
						this.txtMoCode.Text.ToUpper().Trim(),this.cbxItemCode.SelectedItemValue.ToString()))
					{
						ErrorMsg("$CS_Inv_Detail_Exist");//明细资料已经存在于入库单中了
						return;
					}
					
					InvReceive rec = new InvReceive();
				
					rec.Description = this.txtDesc.InnerTextBox.Text.Trim();
					rec.InnerType = ReceiveInnerType.Normal;
					rec.RecType = this.cbxType.ComboBoxData.SelectedItem.ToString();
					rec.ItemCode = this.cbxItemCode.ComboBoxData.SelectedItem.ToString();
					rec.ItemDesc = this.txtItemDesc.InnerTextBox.Text.Trim();
					rec.ModelCode = this.cbxModel.ComboBoxData.SelectedItem.ToString();
					rec.PlanQty = int.Parse(this.txtNum.Text);
					rec.RecNo = this.txtRecNo.Text.Trim().ToUpper();
					rec.MaintainUser = ApplicationService.Current().UserCode;
					rec.MoCode = this.txtMoCode.Text.ToUpper().Trim();

					//判断工单的产品代码和用户选的是否相同
					if(this.txtMoCode.Text.ToUpper().Trim() != string.Empty)
					{	
						BenQGuru.eMES.MOModel.MOFacade mofacade = new MOFacade(this.DataProvider);
						BenQGuru.eMES.Domain.MOModel.MO mo = mofacade.GetMO(rec.MoCode) as BenQGuru.eMES.Domain.MOModel.MO;
						if(mo == null)
						{
							ErrorMsg("$CS_MO_Not_Exist");
							return;
						}
						if(mo.ItemCode != rec.ItemCode)
						{
							ErrorMsg("$Error_ItemCode_NotCompare");
							return;
						}
					}
					this.DataProvider.BeginTransaction();
					_facade.AddInvReceive(rec);

					//加界面显示
					this.AddDataRow(rec);

					this.DataProvider.CommitTransaction();

					this.SucessMsg("$CS_Add_Success");
					this.ClearInput();
				}
				catch(Exception ex)
				{
					this.DataProvider.RollbackTransaction();
					this.ErrorMsg(ex.Message);
				}

				//入库单号发生变化，根据新的入库单号，Load入库单数据
				if(this._nowRecNo != null && this._nowRecNo != this.txtRecNo.Text.Trim())
				{
					LoadData();
				}
			}
			finally
			{
				this.Cursor = System.Windows.Forms.Cursors.Default;
				CloseConnection();
			}
		}


		private void txtRecNo_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				this.LoadData();
                txtMoCode.Focus();
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
				bool isDelete = false;
				try
				{
					this.DataProvider.BeginTransaction();
					InventoryFacade _facade = new InventoryFacade(this.DataProvider);	
					for(int i=0;i<this.ultraGridContent.Rows.Count;i++)
					{
						if(this.ultraGridContent.Rows[i].Cells["*"].Text.ToLower() == "true")
						{
							int cardcount = _facade.GetInvRCardCount(this.ultraGridContent.Rows[i].Cells["NO"].Text,
								int.Parse(this.ultraGridContent.Rows[i].Cells["SEQ"].Text));
							if(cardcount > 0)
								throw new Exception("$Error_Inv_Has_RCard");　///入库单明细已经采集了产品序列号，不能删除

							_facade.RemoveInvReceive(this.ultraGridContent.Rows[i].Cells["NO"].Text,
								int.Parse(this.ultraGridContent.Rows[i].Cells["SEQ"].Text));

							isDelete = true;
						}
					}

					this.DataProvider.CommitTransaction();

					if(isDelete)
					{
						this.LoadData();
						this.SucessMsg("$CS_Delete_Success");
					}
				}
				catch(System.Exception ex)
				{
					this.DataProvider.RollbackTransaction();
					ErrorMsg(ex.Message);
				}
			}
			finally
			{
				this.Cursor = System.Windows.Forms.Cursors.Default;
				CloseConnection();
			}
		}
		#endregion

		private void txtNum_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
				this.btnAdd_Click(null,null);
		}

		//Laws Lu,2006/12/25 焦点进入背景色变为浅绿，移出恢复正常
		private void txtRecNo_Enter(object sender, System.EventArgs e)
		{
			txtRecNo.BackColor = Color.GreenYellow;
		}

		private void txtMoCode_Enter(object sender, System.EventArgs e)
		{
			txtMoCode.BackColor = Color.GreenYellow;
		}

		private void txtNum_Enter(object sender, System.EventArgs e)
		{
			txtNum.BackColor = Color.GreenYellow;
		}

		private void txtRecNo_Leave(object sender, System.EventArgs e)
		{
			txtRecNo.BackColor = Color.White;
		}

		private void txtMoCode_Leave(object sender, System.EventArgs e)
		{
			txtMoCode.BackColor = Color.White;
		}

		private void txtNum_Leave(object sender, System.EventArgs e)
		{
			txtNum.BackColor = Color.White;
		}

		private void FInvReceive_Activated(object sender, System.EventArgs e)
		{
            txtRecNo.Focus();
			txtRecNo.BackColor = Color.GreenYellow;
		}
	}
}
