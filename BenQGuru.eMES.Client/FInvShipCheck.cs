#region using
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

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
#endregion

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FInvPlanate 的摘要说明。
	/// </summary>
	public class FInvShipCheck : System.Windows.Forms.Form
	{
		protected IDomainDataProvider _domainDataProvider;
		private FInfoForm _infoForm;
		private string[] _idList;
		protected InventoryFacade _facade = null;
		private System.Windows.Forms.GroupBox grbShip;
		private UserControl.UCButton btnShipOK;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtShipRCard;
		private System.Windows.Forms.TextBox txtShipCarton;
		private System.Windows.Forms.TextBox txtShipNo;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}
		private void CloseConnection()
		{
			if (this._domainDataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this._domainDataProvider).PersistBroker.CloseConnection(); 
		}

		public FInvShipCheck()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			UserControl.UIStyleBuilder.FormUI(this);	
			_infoForm = ApplicationRun.GetInfoForm();

			_domainDataProvider =ApplicationService.Current().DataProvider;
			this._facade = new InventoryFacade( this.DataProvider );
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}


		public string[] IDList
		{
			get{ return _idList;}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FInvShipCheck));
            this.grbShip = new System.Windows.Forms.GroupBox();
            this.btnShipOK = new UserControl.UCButton();
            this.txtShipRCard = new System.Windows.Forms.TextBox();
            this.txtShipCarton = new System.Windows.Forms.TextBox();
            this.txtShipNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.grbShip.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbShip
            // 
            this.grbShip.Controls.Add(this.btnShipOK);
            this.grbShip.Controls.Add(this.txtShipRCard);
            this.grbShip.Controls.Add(this.txtShipCarton);
            this.grbShip.Controls.Add(this.txtShipNo);
            this.grbShip.Controls.Add(this.label4);
            this.grbShip.Controls.Add(this.label5);
            this.grbShip.Controls.Add(this.label6);
            this.grbShip.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbShip.Location = new System.Drawing.Point(0, 0);
            this.grbShip.Name = "grbShip";
            this.grbShip.Size = new System.Drawing.Size(646, 93);
            this.grbShip.TabIndex = 1;
            this.grbShip.TabStop = false;
            this.grbShip.Text = "出货验证";
            // 
            // btnShipOK
            // 
            this.btnShipOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShipOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnShipOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShipOK.BackgroundImage")));
            this.btnShipOK.ButtonType = UserControl.ButtonTypes.None;
            this.btnShipOK.Caption = "验证出货";
            this.btnShipOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShipOK.Location = new System.Drawing.Point(358, 63);
            this.btnShipOK.Name = "btnShipOK";
            this.btnShipOK.Size = new System.Drawing.Size(88, 22);
            this.btnShipOK.TabIndex = 3;
            this.btnShipOK.Click += new System.EventHandler(this.btnShipOK_Click);
            // 
            // txtShipRCard
            // 
            this.txtShipRCard.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtShipRCard.Location = new System.Drawing.Point(65, 65);
            this.txtShipRCard.Name = "txtShipRCard";
            this.txtShipRCard.Size = new System.Drawing.Size(150, 20);
            this.txtShipRCard.TabIndex = 2;
            this.txtShipRCard.Leave += new System.EventHandler(this.txtShipRCard_Leave);
            this.txtShipRCard.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtShipCarton_KeyUp);
            this.txtShipRCard.Enter += new System.EventHandler(this.txtShipRCard_Enter);
            // 
            // txtShipCarton
            // 
            this.txtShipCarton.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtShipCarton.Location = new System.Drawing.Point(288, 33);
            this.txtShipCarton.Name = "txtShipCarton";
            this.txtShipCarton.Size = new System.Drawing.Size(158, 20);
            this.txtShipCarton.TabIndex = 1;
            this.txtShipCarton.Leave += new System.EventHandler(this.txtShipCarton_Leave);
            this.txtShipCarton.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtShipCarton_KeyUp);
            this.txtShipCarton.Enter += new System.EventHandler(this.txtShipCarton_Enter);
            // 
            // txtShipNo
            // 
            this.txtShipNo.BackColor = System.Drawing.Color.GreenYellow;
            this.txtShipNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtShipNo.Location = new System.Drawing.Point(65, 33);
            this.txtShipNo.Name = "txtShipNo";
            this.txtShipNo.Size = new System.Drawing.Size(152, 20);
            this.txtShipNo.TabIndex = 0;
            this.txtShipNo.Leave += new System.EventHandler(this.txtShipNo_Leave);
            this.txtShipNo.Enter += new System.EventHandler(this.txtShipNo_Enter);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "序列号";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(235, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 21);
            this.label5.TabIndex = 1;
            this.label5.Text = "Carton号";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 22);
            this.label6.TabIndex = 0;
            this.label6.Text = "出货单号";
            // 
            // FInvShipCheck
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(646, 322);
            this.Controls.Add(this.grbShip);
            this.Location = new System.Drawing.Point(400, 400);
            this.Name = "FInvShipCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "出货数据验证";
            this.Load += new System.EventHandler(this.FInvPlanate_Load);
            this.Closed += new System.EventHandler(this.FInvPlanate_Closed);
            this.Activated += new System.EventHandler(this.FInvShipCheck_Activated);
            this.grbShip.ResumeLayout(false);
            this.grbShip.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion


		private void FInvPlanate_Load(object sender, System.EventArgs e)
		{
			_domainDataProvider =ApplicationService.Current().DataProvider;

		}

		private void FInvPlanate_Closed(object sender, System.EventArgs e)
		{
			if (this._domainDataProvider!=null)
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this._domainDataProvider).PersistBroker.CloseConnection();  
			}
		}


		
		protected void ErrorMessage(string msg)
		{			
			_infoForm.Add(new UserControl.Message(UserControl.MessageType.Error,msg));
			BenQGuru.eMES.Web.Helper.SoundPlayer.PlayErrorMusic();
		}

		protected void SucessMessage(string msg)
		{
			_infoForm.Add(new UserControl.Message(UserControl.MessageType.Success,msg));
		}

		private void btnShipOK_Click(object sender, System.EventArgs e)
		{
			#region 检查界面输入
			if(this.txtShipNo.Text.Trim() == string.Empty)
			{
				this.ErrorMessage("$Error_CS_Input_StockIn_TicketNo");
                this.txtShipNo.Focus();
				return;
			}

			if(this.txtShipCarton.Text.Trim() == string.Empty && this.txtShipRCard.Text.Trim() == string.Empty)
			{
				this.ErrorMessage("$CS_PLEASE_INPUT_CARTONNO $CS_OR $CS_PleaseInputID");
                this.txtShipCarton.Focus();
				return;
			}
			#endregion

			bool RecCarton = false;
			bool RecRCard = false;
			if(this.txtShipCarton.Text.Trim() != string.Empty)
			{
				RecCarton = true;
			}
			else if(this.txtShipRCard.Text.Trim() != string.Empty)
			{
				RecRCard = true;
			}

			if(this._facade.CheckShipInv(this.txtShipNo.Text,this.txtShipCarton.Text,this.txtShipRCard.Text))
				this.SucessMessage("$Inv_Carton_Rcard_Exists");
			else
				this.ErrorMessage("$Inv_Carton_Rcard__Not_Exists");

			this.txtShipCarton.Text = string.Empty;
			this.txtShipRCard.Text = string.Empty;

			if(RecCarton)
			{
                this.txtShipCarton.Focus();
			}
			else
			{
                this.txtShipRCard.Focus();
			}
		}

		private void txtShipCarton_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				btnShipOK_Click(null,null);
			}
		}
		//Laws Lu,2006/12/25 焦点进入背景色变为浅绿，移出恢复正常
		private void txtShipNo_Enter(object sender, System.EventArgs e)
		{
			txtShipNo.BackColor = Color.GreenYellow;
		}

		private void txtShipCarton_Leave(object sender, System.EventArgs e)
		{
			txtShipCarton.BackColor = Color.White;
		}

		private void txtShipCarton_Enter(object sender, System.EventArgs e)
		{
			txtShipCarton.BackColor = Color.GreenYellow;
		}

		private void txtShipRCard_Enter(object sender, System.EventArgs e)
		{
			txtShipRCard.BackColor = Color.GreenYellow;
		}

		private void txtShipRCard_Leave(object sender, System.EventArgs e)
		{
			txtShipRCard.BackColor = Color.White;
		}

		private void txtShipNo_Leave(object sender, System.EventArgs e)
		{
			txtShipNo.BackColor = Color.White;
		}

		private void FInvShipCheck_Activated(object sender, System.EventArgs e)
		{
            txtShipNo.Focus();
			txtShipNo.BackColor = Color.GreenYellow;
		}
	}
}
