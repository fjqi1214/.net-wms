using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FInvRCardSwitchType 的摘要说明。
	/// </summary>
	public class FInvRCardSwitchType : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton rbRevNo;
		private System.Windows.Forms.RadioButton rbCarton;
		private System.Windows.Forms.RadioButton rbPCS;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton rbNormalToUnnormal;
		private System.Windows.Forms.RadioButton rbUnnormalToNormal;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtInput;
		private UserControl.UCButton btnOK;
		private UserControl.UCButton btnExit;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel5;
		private UserControl.UCLabelEdit txtCount;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdMain;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private InventoryFacade Facade = null;
		private IDomainDataProvider DataProvider = ApplicationService.Current().DataProvider;

		public FInvRCardSwitchType()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			this.Facade = new InventoryFacade(this.DataProvider);
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			InitDataTable();

			
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FInvRCardSwitchType));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rbPCS = new System.Windows.Forms.RadioButton();
            this.rbCarton = new System.Windows.Forms.RadioButton();
            this.rbRevNo = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbUnnormalToNormal = new System.Windows.Forms.RadioButton();
            this.rbNormalToUnnormal = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExit = new UserControl.UCButton();
            this.btnOK = new UserControl.UCButton();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtCount = new UserControl.UCLabelEdit();
            this.panel5 = new System.Windows.Forms.Panel();
            this.grdMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.rbPCS);
            this.panel1.Controls.Add(this.rbCarton);
            this.panel1.Controls.Add(this.rbRevNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(616, 37);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "采集方式";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rbPCS
            // 
            this.rbPCS.Location = new System.Drawing.Point(260, 7);
            this.rbPCS.Name = "rbPCS";
            this.rbPCS.Size = new System.Drawing.Size(87, 23);
            this.rbPCS.TabIndex = 2;
            this.rbPCS.Text = "PCS";
            // 
            // rbCarton
            // 
            this.rbCarton.Checked = true;
            this.rbCarton.Location = new System.Drawing.Point(165, 6);
            this.rbCarton.Name = "rbCarton";
            this.rbCarton.Size = new System.Drawing.Size(87, 23);
            this.rbCarton.TabIndex = 1;
            this.rbCarton.TabStop = true;
            this.rbCarton.Text = "Carton";
            // 
            // rbRevNo
            // 
            this.rbRevNo.Location = new System.Drawing.Point(73, 7);
            this.rbRevNo.Name = "rbRevNo";
            this.rbRevNo.Size = new System.Drawing.Size(86, 23);
            this.rbRevNo.TabIndex = 0;
            this.rbRevNo.Text = "入库单号";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbUnnormalToNormal);
            this.panel2.Controls.Add(this.rbNormalToUnnormal);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(616, 37);
            this.panel2.TabIndex = 1;
            // 
            // rbUnnormalToNormal
            // 
            this.rbUnnormalToNormal.Location = new System.Drawing.Point(232, 7);
            this.rbUnnormalToNormal.Name = "rbUnnormalToNormal";
            this.rbUnnormalToNormal.Size = new System.Drawing.Size(151, 23);
            this.rbUnnormalToNormal.TabIndex = 2;
            this.rbUnnormalToNormal.Text = "异常品调整为正常品";
            // 
            // rbNormalToUnnormal
            // 
            this.rbNormalToUnnormal.Checked = true;
            this.rbNormalToUnnormal.Location = new System.Drawing.Point(73, 7);
            this.rbNormalToUnnormal.Name = "rbNormalToUnnormal";
            this.rbNormalToUnnormal.Size = new System.Drawing.Size(140, 23);
            this.rbNormalToUnnormal.TabIndex = 1;
            this.rbNormalToUnnormal.TabStop = true;
            this.rbNormalToUnnormal.Text = "正常品调整为异常品";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "调整方式";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnExit);
            this.panel3.Controls.Add(this.btnOK);
            this.panel3.Controls.Add(this.txtInput);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 409);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(616, 45);
            this.panel3.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(400, 15);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.ButtonType = UserControl.ButtonTypes.None;
            this.btnOK.Caption = "确定";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Location = new System.Drawing.Point(307, 15);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 22);
            this.btnOK.TabIndex = 1;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(73, 15);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(214, 20);
            this.txtInput.TabIndex = 0;
            this.txtInput.Leave += new System.EventHandler(this.txtInput_Leave);
            this.txtInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyUp);
            this.txtInput.Enter += new System.EventHandler(this.txtInput_Enter);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "输入框";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtCount);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(510, 74);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(106, 335);
            this.panel4.TabIndex = 3;
            // 
            // txtCount
            // 
            this.txtCount.AllowEditOnlyChecked = true;
            this.txtCount.Caption = "数量总计";
            this.txtCount.Checked = false;
            this.txtCount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtCount.EditType = UserControl.EditTypes.String;
            this.txtCount.Location = new System.Drawing.Point(0, 312);
            this.txtCount.MaxLength = 40;
            this.txtCount.Multiline = false;
            this.txtCount.Name = "txtCount";
            this.txtCount.PasswordChar = '\0';
            this.txtCount.ReadOnly = true;
            this.txtCount.ShowCheckBox = false;
            this.txtCount.Size = new System.Drawing.Size(106, 23);
            this.txtCount.TabIndex = 0;
            this.txtCount.TabNext = true;
            this.txtCount.Value = "0";
            this.txtCount.WidthType = UserControl.WidthTypes.Tiny;
            this.txtCount.XAlign = 64;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.grdMain);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 74);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(510, 335);
            this.panel5.TabIndex = 4;
            // 
            // grdMain
            // 
            this.grdMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(510, 335);
            this.grdMain.TabIndex = 0;
            this.grdMain.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.grdMain_InitializeLayout);
            // 
            // FInvRCardSwitchType
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(616, 454);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FInvRCardSwitchType";
            this.Text = "库存品状态转换";
            this.Load += new System.EventHandler(this.FInvRCardSwitchType_Load);
            this.Activated += new System.EventHandler(this.FInvRCardSwitchType_Activated);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void txtInput_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			try
			{
				if(e.KeyCode != Keys.Enter || this.txtInput.Text == "")
					return;

				string collectType = GetCollectType();

				DataRow row = null;

				if(collectType == CollectType.RevNO)
				{
					row = SwitchRecNo();
				}
				else if(collectType == CollectType.Carton)
				{
					row = SwitchCarton();
				}
				else if(collectType == CollectType.PCS)
				{
					row = SwitchPCS();
				}

				this.Table.Rows.Add(row);

				this.Table.AcceptChanges();

				this.TotalCount += int.Parse(row["Count"].ToString());

				this.txtInput.Text = "";
                this.txtInput.Focus();
			}
			catch(Exception ex)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));
			}
		}

		private void ClearControlValues()
		{
			this.txtInput.Text = "";
			this.TotalCount = 0;
			this.Table.Rows.Clear();
			this.Table.AcceptChanges();
		}

		private string GetCollectType()
		{
			if(this.rbRevNo.Checked)
				return CollectType.RevNO;
			else if(this.rbCarton.Checked)
				return CollectType.Carton;
			else if(this.rbPCS.Checked)
				return CollectType.PCS;

			return "";
		}

		private int TotalCount
		{
			get
			{
				return int.Parse(this.txtCount.Value);
			}
			set
			{
				this.txtCount.Value = value.ToString();
			}
		}

		private void CheckInput(string Input)
		{
			DataRow[] rows = this.Table.Select(string.Format("Input='{0}'",Input));

			if(rows != null && rows.Length > 0)
				throw new Exception("$Input_Has_In_Queue");
		}

		private DataRow SwitchRecNo()
		{
			object[] rcards = this.Facade.GetInvRCardByRecNo(this.txtInput.Text);

			if(rcards == null || rcards.Length == 0)
				throw new Exception("$RecNo_Not_Exists");

			string switchType = GetSwitchType();
			
			foreach(InvRCard rcard in rcards)
			{
				this.Facade.CheckRCard(rcard,switchType);
			}

			DataRow row = this.Table.NewRow();
			row["Input"] = this.txtInput.Text;
			row["ItemCode"] = ((InvRCard)rcards[0]).ItemCode;
			row["Count"] = rcards.Length;
			row["CollectType"] = GetCollectType();
			row["SwitchType"] = GetSwitchType();

			return row;
		}

		private DataRow SwitchCarton()
		{
			object[] rcards = this.Facade.GetInvRCardByCartonCode(this.txtInput.Text);

			if(rcards == null || rcards.Length == 0)
				throw new Exception("$CartonCode_Not_Exists");

			string switchType = GetSwitchType();
			
			foreach(InvRCard rcard in rcards)
			{
				this.Facade.CheckRCard(rcard,switchType);
			}

			DataRow row = this.Table.NewRow();
			row["Input"] = this.txtInput.Text;
			row["ItemCode"] = ((InvRCard)rcards[0]).ItemCode;
			row["Count"] = rcards.Length;
			row["CollectType"] = GetCollectType();
			row["SwitchType"] = GetSwitchType();

			return row;
		}

		private DataRow SwitchPCS()
		{
			object[] rcards = this.Facade.GetInvRCardByRCard(this.txtInput.Text);

			if(rcards == null || rcards.Length == 0)
				throw new Exception("$RCard_Not_Exists");

			string switchType = GetSwitchType();
			
			foreach(InvRCard rcard in rcards)
			{
				this.Facade.CheckRCard(rcard,switchType);
			}

			DataRow row = this.Table.NewRow();
			row["Input"] = this.txtInput.Text;
			row["ItemCode"] = ((InvRCard)rcards[0]).ItemCode;
			row["Count"] = rcards.Length;
			row["CollectType"] = GetCollectType();
			row["SwitchType"] = GetSwitchType();

			return row;
		}

		
		
		private DataTable Table = new DataTable();

		private void InitDataTable()
		{
			this.Table.Columns.Add("Input");
			this.Table.Columns.Add("ItemCode");
			this.Table.Columns.Add("Count");
			this.Table.Columns.Add("CollectType");
			this.Table.Columns.Add("SwitchType");
			this.Table.AcceptChanges();
		}

		private string GetSwitchType()
		{
			if(this.rbNormalToUnnormal.Checked)
				return SwitchType.NormalToUnnormal;
			else if(this.rbUnnormalToNormal.Checked)
				return SwitchType.UnnormalToNormal;

			return "";
		}


		private void FInvRCardSwitchType_Load(object sender, System.EventArgs e)
		{
			UserControl.UIStyleBuilder.FormUI(this);

			UserControl.UIStyleBuilder.GridUI(this.grdMain);

			grdMain.DataSource = null;
			DataView dv = Table.DefaultView;
			//dv.Sort = "ModelCode,ItemCode";
			grdMain.DataSource = dv;
			//grdMain.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
			grdMain.DisplayLayout.Bands[0].Columns["CollectType"].Hidden = true;
			grdMain.DisplayLayout.Bands[0].Columns["SwitchType"].Hidden = true;

            this.txtInput.Focus();
		}

		private void grdMain_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
		{
			UltraWinGridHelper gridHelper = new UltraWinGridHelper(this.grdMain);
			gridHelper.AddCommonColumn("Input","采集对象号码");
			gridHelper.AddCommonColumn("CollectType","CollectType");
			gridHelper.AddCommonColumn("SwitchType","SwitchType");
			gridHelper.AddCommonColumn("ItemCode","产品代码");
			gridHelper.AddCommonColumn("Count","产品数量");
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Facade.SwitchRcardType(this.Table);

				ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Success,"$CS_Save_Success"));

				ClearControlValues();
                this.txtInput.Focus();
			}
			catch(Exception ex)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));
			}
		}
		//Laws Lu,2006/12/25 焦点进入背景色变为浅绿，移出恢复正常
		private void txtInput_Enter(object sender, System.EventArgs e)
		{
			txtInput.BackColor = Color.GreenYellow;
		}

		private void txtInput_Leave(object sender, System.EventArgs e)
		{
			txtInput.BackColor = Color.White;
		}

		private void FInvRCardSwitchType_Activated(object sender, System.EventArgs e)
		{
            txtInput.Focus();
			txtInput.BackColor = Color.GreenYellow;
		}

	}

	
}
