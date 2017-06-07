#region system
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System.Data;
#endregion

#region Project
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.DataCollect;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.MOModel;
#endregion


namespace BenQGuru.eMES.Client
{
	public class FPackCarton : Form
	{
		private Domain.Package.CARTONINFO _carton = null;
		private DataTable dtIDList = new DataTable();

		private UserControl.UCLabelEdit ucLabEdit2;
		private System.ComponentModel.IContainer components = null;
		private UserControl.UCLabelEdit ucLabelEdit1;
		private System.Windows.Forms.Panel panel1;
		private UserControl.UCLabelEdit txtCartonNO;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
		private UserControl.UCButton uBtnExit;
		private UserControl.UCLabelEdit txtCapcity;
		private UserControl.UCLabelEdit txtCollected;
		private System.Windows.Forms.Panel panel4;
		private UserControl.UCMessage ucMessage;
		private UserControl.UCLabelEdit txtRCard;
		private System.Windows.Forms.CheckBox chkCodex;
		private UserControl.UCButton btnEnable;
		private UserControl.UCLabelEdit txtMemo;

        private string _FunctionName = string.Empty;

		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		public FPackCarton()
		{
			// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();

			ucMessage.AddEx(this._FunctionName, "", new UserControl.Message(">>$CS_PLEASE_INPUT_CARTONNO"),false);

			UserControl.UIStyleBuilder.FormUI(this);	
			UserControl.UIStyleBuilder.GridUI(ultraGridMain);
		   
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPackCarton));
            this.ucLabEdit2 = new UserControl.UCLabelEdit();
            this.ucLabelEdit1 = new UserControl.UCLabelEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMemo = new UserControl.UCLabelEdit();
            this.btnEnable = new UserControl.UCButton();
            this.chkCodex = new System.Windows.Forms.CheckBox();
            this.txtCapcity = new UserControl.UCLabelEdit();
            this.txtCartonNO = new UserControl.UCLabelEdit();
            this.txtCollected = new UserControl.UCLabelEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.uBtnExit = new UserControl.UCButton();
            this.txtRCard = new UserControl.UCLabelEdit();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ucMessage = new UserControl.UCMessage();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucLabEdit2
            // 
            this.ucLabEdit2.AllowEditOnlyChecked = true;
            this.ucLabEdit2.Caption = "包装数量";
            this.ucLabEdit2.Checked = false;
            this.ucLabEdit2.EditType = UserControl.EditTypes.String;
            this.ucLabEdit2.Location = new System.Drawing.Point(161, -16);
            this.ucLabEdit2.MaxLength = 40;
            this.ucLabEdit2.Multiline = false;
            this.ucLabEdit2.Name = "ucLabEdit2";
            this.ucLabEdit2.PasswordChar = '\0';
            this.ucLabEdit2.ReadOnly = false;
            this.ucLabEdit2.ShowCheckBox = false;
            this.ucLabEdit2.Size = new System.Drawing.Size(194, 56);
            this.ucLabEdit2.TabIndex = 16;
            this.ucLabEdit2.TabNext = true;
            this.ucLabEdit2.Value = "";
            this.ucLabEdit2.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEdit2.XAlign = 222;
            // 
            // ucLabelEdit1
            // 
            this.ucLabelEdit1.AllowEditOnlyChecked = true;
            this.ucLabelEdit1.Caption = "输入标示";
            this.ucLabelEdit1.Checked = false;
            this.ucLabelEdit1.EditType = UserControl.EditTypes.String;
            this.ucLabelEdit1.Location = new System.Drawing.Point(358, 16);
            this.ucLabelEdit1.MaxLength = 40;
            this.ucLabelEdit1.Multiline = false;
            this.ucLabelEdit1.Name = "ucLabelEdit1";
            this.ucLabelEdit1.PasswordChar = '\0';
            this.ucLabelEdit1.ReadOnly = false;
            this.ucLabelEdit1.ShowCheckBox = false;
            this.ucLabelEdit1.Size = new System.Drawing.Size(194, 24);
            this.ucLabelEdit1.TabIndex = 1;
            this.ucLabelEdit1.TabNext = true;
            this.ucLabelEdit1.Value = "";
            this.ucLabelEdit1.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEdit1.XAlign = 419;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtMemo);
            this.panel1.Controls.Add(this.btnEnable);
            this.panel1.Controls.Add(this.chkCodex);
            this.panel1.Controls.Add(this.txtCapcity);
            this.panel1.Controls.Add(this.txtCartonNO);
            this.panel1.Controls.Add(this.txtCollected);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(720, 82);
            this.panel1.TabIndex = 285;
            // 
            // txtMemo
            // 
            this.txtMemo.AllowEditOnlyChecked = true;
            this.txtMemo.BackColor = System.Drawing.Color.Gainsboro;
            this.txtMemo.Caption = "备注";
            this.txtMemo.Checked = false;
            this.txtMemo.EditType = UserControl.EditTypes.String;
            this.txtMemo.Location = new System.Drawing.Point(340, 15);
            this.txtMemo.MaxLength = 40;
            this.txtMemo.Multiline = false;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.PasswordChar = '\0';
            this.txtMemo.ReadOnly = false;
            this.txtMemo.ShowCheckBox = false;
            this.txtMemo.Size = new System.Drawing.Size(197, 22);
            this.txtMemo.TabIndex = 203;
            this.txtMemo.TabNext = true;
            this.txtMemo.TabStop = false;
            this.txtMemo.Value = "";
            this.txtMemo.WidthType = UserControl.WidthTypes.Long;
            this.txtMemo.XAlign = 371;
            // 
            // btnEnable
            // 
            this.btnEnable.BackColor = System.Drawing.SystemColors.Control;
            this.btnEnable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEnable.BackgroundImage")));
            this.btnEnable.ButtonType = UserControl.ButtonTypes.None;
            this.btnEnable.Caption = "锁定";
            this.btnEnable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnable.Location = new System.Drawing.Point(247, 15);
            this.btnEnable.Name = "btnEnable";
            this.btnEnable.Size = new System.Drawing.Size(73, 20);
            this.btnEnable.TabIndex = 202;
            this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // chkCodex
            // 
            this.chkCodex.Location = new System.Drawing.Point(340, 52);
            this.chkCodex.Name = "chkCodex";
            this.chkCodex.Size = new System.Drawing.Size(140, 22);
            this.chkCodex.TabIndex = 201;
            this.chkCodex.Text = "检查Carton 与序列号算法";
            this.chkCodex.Visible = false;
            // 
            // txtCapcity
            // 
            this.txtCapcity.AllowEditOnlyChecked = true;
            this.txtCapcity.BackColor = System.Drawing.Color.Gainsboro;
            this.txtCapcity.Caption = "Carton容量";
            this.txtCapcity.Checked = false;
            this.txtCapcity.EditType = UserControl.EditTypes.Integer;
            this.txtCapcity.Location = new System.Drawing.Point(24, 52);
            this.txtCapcity.MaxLength = 40;
            this.txtCapcity.Multiline = false;
            this.txtCapcity.Name = "txtCapcity";
            this.txtCapcity.PasswordChar = '\0';
            this.txtCapcity.ReadOnly = true;
            this.txtCapcity.ShowCheckBox = false;
            this.txtCapcity.Size = new System.Drawing.Size(144, 22);
            this.txtCapcity.TabIndex = 100;
            this.txtCapcity.TabNext = true;
            this.txtCapcity.TabStop = false;
            this.txtCapcity.Value = "";
            this.txtCapcity.WidthType = UserControl.WidthTypes.Small;
            this.txtCapcity.XAlign = 85;
            // 
            // txtCartonNO
            // 
            this.txtCartonNO.AllowEditOnlyChecked = true;
            this.txtCartonNO.BackColor = System.Drawing.Color.Gainsboro;
            this.txtCartonNO.Caption = "Carton号";
            this.txtCartonNO.Checked = false;
            this.txtCartonNO.EditType = UserControl.EditTypes.String;
            this.txtCartonNO.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCartonNO.Location = new System.Drawing.Point(24, 15);
            this.txtCartonNO.MaxLength = 40;
            this.txtCartonNO.Multiline = false;
            this.txtCartonNO.Name = "txtCartonNO";
            this.txtCartonNO.PasswordChar = '\0';
            this.txtCartonNO.ReadOnly = false;
            this.txtCartonNO.ShowCheckBox = false;
            this.txtCartonNO.Size = new System.Drawing.Size(218, 22);
            this.txtCartonNO.TabIndex = 1;
            this.txtCartonNO.TabNext = true;
            this.txtCartonNO.Value = "";
            this.txtCartonNO.WidthType = UserControl.WidthTypes.Long;
            this.txtCartonNO.XAlign = 75;
            this.txtCartonNO.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCartonNO_TxtboxKeyPress);
            // 
            // txtCollected
            // 
            this.txtCollected.AllowEditOnlyChecked = true;
            this.txtCollected.BackColor = System.Drawing.Color.Gainsboro;
            this.txtCollected.Caption = "已采集数量";
            this.txtCollected.Checked = false;
            this.txtCollected.EditType = UserControl.EditTypes.Integer;
            this.txtCollected.Location = new System.Drawing.Point(176, 52);
            this.txtCollected.MaxLength = 40;
            this.txtCollected.Multiline = false;
            this.txtCollected.Name = "txtCollected";
            this.txtCollected.PasswordChar = '\0';
            this.txtCollected.ReadOnly = true;
            this.txtCollected.ShowCheckBox = false;
            this.txtCollected.Size = new System.Drawing.Size(144, 22);
            this.txtCollected.TabIndex = 200;
            this.txtCollected.TabNext = true;
            this.txtCollected.TabStop = false;
            this.txtCollected.Value = "";
            this.txtCollected.WidthType = UserControl.WidthTypes.Small;
            this.txtCollected.XAlign = 237;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.uBtnExit);
            this.panel2.Controls.Add(this.txtRCard);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 433);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(720, 52);
            this.panel2.TabIndex = 286;
            // 
            // uBtnExit
            // 
            this.uBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.uBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uBtnExit.BackgroundImage")));
            this.uBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.uBtnExit.Caption = "退出";
            this.uBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnExit.Location = new System.Drawing.Point(293, 15);
            this.uBtnExit.Name = "uBtnExit";
            this.uBtnExit.Size = new System.Drawing.Size(74, 20);
            this.uBtnExit.TabIndex = 0;
            // 
            // txtRCard
            // 
            this.txtRCard.AllowEditOnlyChecked = true;
            this.txtRCard.BackColor = System.Drawing.Color.Gainsboro;
            this.txtRCard.Caption = "SN输入框";
            this.txtRCard.Checked = false;
            this.txtRCard.EditType = UserControl.EditTypes.String;
            this.txtRCard.Location = new System.Drawing.Point(4, 15);
            this.txtRCard.MaxLength = 4000;
            this.txtRCard.Multiline = false;
            this.txtRCard.Name = "txtRCard";
            this.txtRCard.PasswordChar = '\0';
            this.txtRCard.ReadOnly = false;
            this.txtRCard.ShowCheckBox = false;
            this.txtRCard.Size = new System.Drawing.Size(218, 22);
            this.txtRCard.TabIndex = 3;
            this.txtRCard.TabNext = false;
            this.txtRCard.Value = "";
            this.txtRCard.WidthType = UserControl.WidthTypes.Long;
            this.txtRCard.XAlign = 55;
            this.txtRCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRCard_TxtboxKeyPress);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ultraGridMain);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 82);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(720, 223);
            this.panel3.TabIndex = 287;
            // 
            // ultraGridMain
            // 
            this.ultraGridMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMain.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridMain.Location = new System.Drawing.Point(0, 0);
            this.ultraGridMain.Name = "ultraGridMain";
            this.ultraGridMain.Size = new System.Drawing.Size(720, 223);
            this.ultraGridMain.TabIndex = 300;
            this.ultraGridMain.TabStop = false;
            this.ultraGridMain.Text = "Carton中产品列表";
            this.ultraGridMain.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMain_InitializeLayout);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ucMessage);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 305);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(720, 128);
            this.panel4.TabIndex = 288;
            // 
            // ucMessage
            // 
            this.ucMessage.AutoScroll = true;
            this.ucMessage.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessage.ButtonVisible = false;
            this.ucMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessage.Location = new System.Drawing.Point(0, 0);
            this.ucMessage.Name = "ucMessage";
            this.ucMessage.Size = new System.Drawing.Size(720, 128);
            this.ucMessage.TabIndex = 175;
            // 
            // FPackCarton
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(720, 485);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FPackCarton";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carton包装采集";
            this.Load += new System.EventHandler(this.FPack_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion


		#region form的初始化
		private void InitForm()
		{
			
		}

		#endregion


		#region 页面事件

		private void FPack_Load(object sender, System.EventArgs e)
		{
//			InitForm();
			InitializeGrid();

            this._FunctionName = this.Text;
		}
		
		private void ultraGridMain_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
		{
			UltraWinGridHelper ultraWinGridHelper = new UltraWinGridHelper(this.ultraGridMain);
			//ultraWinGridHelper.AddCheckColumn("checkbox","*");
			ultraWinGridHelper.AddCommonColumn("itemcode","产品");
			ultraWinGridHelper.AddCommonColumn("mocode","工单");
			//ultraWinGridHelper.AddCommonColumn("stepsequence","生产线");
			ultraWinGridHelper.AddCommonColumn("runningcard","产品序列号");
			//ultraWinGridHelper.AddCommonColumn("cartonno","Carton号");
			//ultraWinGridHelper.AddCommonColumn("collecttype","采集类型");
		}

		private void txtCartonNO_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				string cartonno = txtCartonNO.Value.Trim().ToUpper();

				if(cartonno == String.Empty)
				{
					ucMessage.AddEx(this._FunctionName, this.txtCartonNO.Caption + " " + this.txtCartonNO.Value,
                        new UserControl.Message("$CS_PLEASE_INPUT_CARTONNO"), false);

					txtCartonNO.TextFocus(false, true);

					return;
				}

				ProcessCarton(cartonno);

				btnEnable_Click(sender,e);

				txtRCard.TextFocus(false, true);

				//SendKeys.Send("+{TAB}");
                //Remove UCLabel.SelectAll;
			}
		}

		private void txtRCard_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				string rcard = txtRCard.Value.Trim().ToUpper();
				bool validate = true;

				if(chkCodex.Checked == true)
				{
					validate = ValidateCartonCodex(rcard);
				}

				if(validate)
				{
					ProcessRcard(rcard);
					//UpdateCartonQty();
				}
				else
				{
                    ucMessage.AddEx(this._FunctionName, this.txtRCard.Caption + " " + this.txtRCard.Value, new UserControl.Message(MessageType.Error
						,"$CS_RCARD_NOT_BELONG_CARTON"
                        + ",$CS_Param_ID =" + txtRCard.Value.Trim().ToUpper()), false);
                    ucMessage.AddEx(this._FunctionName, this.txtRCard.Caption + " " + this.txtRCard.Value, new UserControl.Message(MessageType.Error
                        , "$CS_CARTON_NO =" + txtCartonNO.Value.Trim()), false);

				}

				txtRCard.TextFocus(true, true);
			}
		}

		private void btnEnable_Click(object sender, System.EventArgs e)
		{
			if(txtCartonNO.Enabled)
			{
				txtCartonNO.Enabled = false;
				btnEnable.Caption = "解除锁定";
			}
			else
			{
				txtCartonNO.Enabled = true;
				//txtCartonNO.TextFocus(true, true);
				btnEnable.Caption = "锁定";
			}
		}

		#endregion

		
		//Initialize Grid and build columns
		private void InitializeGrid()
		{
			dtIDList.Columns.Clear();
			//dtIDList.Columns.Add("checkbox",typeof(System.Boolean));
			dtIDList.Columns.Add("itemcode",typeof(string)).ReadOnly = true;
			dtIDList.Columns.Add("mocode",typeof(string)).ReadOnly = true;
			//dtIDList.Columns.Add("stepsequence",typeof(string)).ReadOnly = true;
			dtIDList.Columns.Add("runningcard",typeof(string)).ReadOnly = true;
			//dtIDList.Columns.Add("cartonno",typeof(string)).ReadOnly = true;
			//dtIDList.Columns.Add("collecttype",typeof(string)).ReadOnly = true;

			this.ultraGridMain.DataSource = dtIDList;
		}

		//update grid display
		private void UpdateGrid(string cartonno)
		{
			object[] objs = (new DataCollectFacade(DataProvider)).GetSimulationFromCarton(cartonno);

			if(objs != null && objs.Length > 0)
			{
				BindGrid(objs);
			}
		}
		//add data to grid
		private void BindGrid(object[] objs)
		{
			dtIDList.Clear();
			foreach(Domain.DataCollect.Simulation sim in objs)
			{
				dtIDList.Rows.Add(new object[]{
												  sim.ItemCode
												  ,sim.MOCode	
												  ,sim.RunningCard
											  });
			}
			dtIDList.AcceptChanges();
		}

		
		//Process for Carton No
		private void ProcessCarton(string cartonno)
		{
			//Laws Lu,2006/05/27	包装到Carton
			Package.PackageFacade pf = new BenQGuru.eMES.Package.PackageFacade(DataProvider);
			
			object objCarton =  pf.GetCARTONINFO(cartonno);
			

			Domain.Package.CARTONINFO carton = null;
			if(objCarton != null)
			{
				carton = objCarton as  Domain.Package.CARTONINFO;
				if(carton.CAPACITY <= carton.COLLECTED)
				{
                    ucMessage.AddEx(this._FunctionName, this.txtCartonNO.Caption + " " + this.txtCartonNO.Value, new UserControl.Message(MessageType.Error, "$CARTON_ALREADY_FILL_OUT"), false);
				}
				else
				{
                    ucMessage.AddEx(this._FunctionName, this.txtRCard.Caption + " " + this.txtRCard.Value, new UserControl.Message(">>$CS_Please_Input_RunningCard"), false);
				}
				txtMemo.Value = carton.EATTRIBUTE1;
				//pf.UpdateCARTONINFO(carton);
				this.UpdateGrid(cartonno);
			}
			else if(cartonno != String.Empty)
			{
				//carton = objCarton as  Domain.Package.CARTONINFO;

				carton = new BenQGuru.eMES.Domain.Package.CARTONINFO();
				carton.PKCARTONID = System.Guid.NewGuid().ToString();
				carton.CAPACITY = 0;//((new ItemFacade(DataProvider)).GetItem(oqcLotAddIDEventArgs.CurrentItemRoute2OP.ItemCode) as Item).ItemCartonQty;
				carton.COLLECTED = 0;
				carton.PKCARTONID = System.Guid.NewGuid().ToString().ToUpper();
				carton.CARTONNO = cartonno;
				carton.MUSER = ApplicationService.Current().UserCode;

				//Laws Lu,2006/11/13 uniform system collect date
				DBDateTime dbDateTime;
				
				dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
				
				carton.MDATE = dbDateTime.DBDate;
				carton.MTIME = dbDateTime.DBTime;
				carton.EATTRIBUTE1 = FormatHelper.CleanString( this.txtMemo.Value, 40);

                ucMessage.AddEx(this._FunctionName, this.txtRCard.Caption + " " + this.txtRCard.Value, new UserControl.Message(">>$CS_Please_Input_RunningCard"), false);
				//carton.

				//pf.AddCARTONINFO(carton);
			}

			_carton  = carton;

			UpdateCartonQty();

			
			//End
		}

	
		//Process for Running Card
		private void ProcessRcard(string rcard)
		{
			//1.Get simulation and check it
			DataCollectFacade dcf = new DataCollectFacade(DataProvider);
			
			//Laws Lu,2007/01/05,新增	缓解性能问题
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			//ActionOnLineHelper onlineHelper = new ActionOnLineHelper(DataProvider);
			Domain.DataCollect.Simulation sim = (new DataCollectFacade(DataProvider)).GetSimulation(rcard) 
				as Domain.DataCollect.Simulation;

			if(sim == null)//rcard must exist
			{
                ucMessage.AddEx(this._FunctionName, this.txtRCard.Caption + " " + this.txtRCard.Value, new UserControl.Message(MessageType.Error, "$NoSimulation $CS_Param_ID =" + txtRCard.Value.Trim().ToUpper()), false);
				//txtRCard.TextFocus(true, true);
				return;
			}

			if(sim != null && sim.CartonCode != String.Empty)//rcard must not link with any carton
			{
                ucMessage.AddEx(this._FunctionName, this.txtRCard.Caption + " " + this.txtRCard.Value, new UserControl.Message(MessageType.Error
                    , "$CS_RCARD_ALREADY_BELONGTO_CARTON $CS_Param_ID =" + txtRCard.Value.Trim().ToUpper()), false);
				//txtRCard.TextFocus(true, true);
				return;
			}

			sim.CartonCode = txtCartonNO.Value.Trim().ToUpper();

			//2.Get cartion
			int flag = 0;//Not Need Add
            Domain.MOModel.Item item = ((new ItemFacade(DataProvider)).GetItem(sim.ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID) as Domain.MOModel.Item);
			if(_carton.CAPACITY == 0)//Get carton capacity by item
			{
				flag = 1;
				
				if(item.ItemCartonQty == 0)
				{
                    ucMessage.AddEx(this._FunctionName, this.txtCartonNO.Caption + " " + this.txtCartonNO.Value, new UserControl.Message(MessageType.Error
                        , "$CS_PLEASE_MAINTEIN_ITEMCARTON $CS_Param_ID =" + txtRCard.Value.Trim().ToUpper()), false);
					//txtRCard.TextFocus(true, true);
					return;
				}
				_carton.CAPACITY = item .ItemCartonQty;
			}

			//3.set simulation cartoncode by current carton
			DataProvider.BeginTransaction();
			try
			{
				bool isSuccessful = true;

				Package.PackageFacade pf = new BenQGuru.eMES.Package.PackageFacade(DataProvider);
				if(flag == 1)//need add new carton and update simulation
				{
					_carton.COLLECTED = 1;

					if(isSuccessful)
					{
						//Laws Lu,2006/06/30 support memo
						_carton.EATTRIBUTE1 = txtMemo.Value.ToUpper().Trim();
						
						//更新Carton号
						dcf.UpdateSimulation(sim);

						//ProductInfo product = onlineHelper.GetIDInfoBySimulation(sim);
//						Domain.DataCollect.SimulationReport simReport = onlineHelper.FillSimulationReport(product);
//						dcf.UpdateSimulationReport(simReport);
						dcf.UpdateSimulationReportCartonNo(sim.RunningCard,sim.MOCode,_carton.CARTONNO);

						//建立新Carton
						pf.AddCARTONINFO(_carton);
					}
				}
				else if(_carton.CAPACITY > _carton.COLLECTED)//just need update carton and simulation
				{
					if(Convert.ToInt32(_carton.CAPACITY) != item.ItemCartonQty)
					{
						isSuccessful = false;
                        ucMessage.AddEx(this._FunctionName, this.txtCartonNO.Caption + " " + this.txtCartonNO.Value, new UserControl.Message(MessageType.Error
							,"$CS_ITEMCARTON_NOT_MATCH_CARTON $CS_Param_ID =" + txtRCard.Value.Trim().ToUpper()
                            + " $CS_CARTON_NO =" + _carton.CARTONNO), false);
					}
					
					if(isSuccessful)
					{
						//Laws Lu,2006/06/30 support memo
						_carton.EATTRIBUTE1 = txtMemo.Value.ToUpper().Trim();
						//更新Carton号
						dcf.UpdateSimulation(sim);
						//ProductInfo product = onlineHelper.GetIDInfoBySimulation(sim);
//						Domain.DataCollect.SimulationReport simReport = onlineHelper.FillSimulationReport(product);
//						dcf.UpdateSimulationReport(simReport);
						dcf.UpdateSimulationReportCartonNo(sim.RunningCard,sim.MOCode,_carton.CARTONNO);
						//更新Carton中数量
						//Laws Lu,2006/06/23	modify support update memo
						pf.UpdateCollected(_carton.CARTONNO,_carton.EATTRIBUTE1);

						/* added by jessie lee, 2006/6/16
						 * 更新备注 */
						//pf.UpdateCARTONINFO(_carton);
					}
				}
				else
				{
					isSuccessful = false;
                    ucMessage.AddEx(this._FunctionName, this.txtCartonNO.Caption + " " + this.txtCartonNO.Value, new UserControl.Message(MessageType.Error
                        , "$CARTON_ALREADY_FILL_OUT"), false);
				}

				if(isSuccessful)
				{
					DataProvider.CommitTransaction();

                    ucMessage.AddEx(this._FunctionName, this.txtCartonNO.Caption + " " + this.txtCartonNO.Value, new UserControl.Message(MessageType.Success
                        , "$CS_RCARD_CARTON_SUCCESS $CS_Param_ID =" + txtRCard.Value.Trim().ToUpper()), false);

					ProcessCarton(txtCartonNO.Value.Trim().ToUpper());
//					if(_carton.COLLECTED != 1)
//					{
//						_carton.COLLECTED = _carton.COLLECTED + 1;
//					}
					//UpdateCartonQty();
				}
				else
				{
					DataProvider.RollbackTransaction();
				}
			}
			catch(Exception ex)
			{
                ucMessage.AddEx(this._FunctionName, this.txtRCard.Caption + " " + this.txtRCard.Value, new UserControl.Message(ex), false);
				DataProvider.RollbackTransaction();
			}
			finally
			{
				//Laws Lu,2007/01/05,新增	缓解性能问题
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
//				(DataProvider as SQLDomainDataProvider).PersistBroker.CloseConnection();

				txtRCard.TextFocus(true, true);
			}

		}

		private void UpdateCartonQty()
		{
			txtCapcity.Value = _carton.CAPACITY.ToString();
			txtCollected.Value = _carton.COLLECTED.ToString();
		}

		private bool ValidateCartonCodex(string rcard)
		{
			bool bResult = true;
			
			if(rcard != String.Empty)
			{
				string input = rcard.Substring(rcard.Length - 4,4);
				long lctnNO = FormatHelper.DecFrom36(input);

				long currentCTNNO = long.Parse(txtCartonNO.Value.Trim());

				if(lctnNO > currentCTNNO + 10 || lctnNO < currentCTNNO)
				{
					bResult = false;
				}

			}
			return bResult;
		}
	}
}

