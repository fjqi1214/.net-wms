using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FSNCheck 的摘要说明。
	/// </summary>
	public class FSNCheck : System.Windows.Forms.Form
	{
		private const string Pass = "PASS";
		private const string Fail = "FAIL";

		private UserControl.UCLabelEdit txtRunningCard;
		private UserControl.UCLabelEdit txtCheckCard;
		private System.Windows.Forms.Label lblResult;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FSNCheck()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			UserControl.UIStyleBuilder.FormUI(this);	

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
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

//		private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
//		public IDomainDataProvider DataProvider
//		{
//			get
//			{
//				return _domainDataProvider;
//			}
//		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.txtRunningCard = new UserControl.UCLabelEdit();
			this.txtCheckCard = new UserControl.UCLabelEdit();
			this.lblResult = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtRunningCard
			// 
			this.txtRunningCard.AllowEditOnlyChecked = true;
			this.txtRunningCard.Caption = "原产品序列号";
			this.txtRunningCard.Checked = false;
			this.txtRunningCard.EditType = UserControl.EditTypes.String;
			this.txtRunningCard.Location = new System.Drawing.Point(200, 24);
			this.txtRunningCard.MaxLength = 40;
			this.txtRunningCard.Multiline = false;
			this.txtRunningCard.Name = "txtRunningCard";
			this.txtRunningCard.PasswordChar = '\0';
			this.txtRunningCard.ReadOnly = false;
			this.txtRunningCard.ShowCheckBox = false;
			this.txtRunningCard.Size = new System.Drawing.Size(287, 24);
			this.txtRunningCard.TabIndex = 3;
			this.txtRunningCard.TabNext = true;
			this.txtRunningCard.Value = "";
			this.txtRunningCard.WidthType = UserControl.WidthTypes.Long;
			this.txtRunningCard.XAlign = 287;
			this.txtRunningCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRunningCard_TxtboxKeyPress);
			// 
			// txtCheckCard
			// 
			this.txtCheckCard.AllowEditOnlyChecked = true;
			this.txtCheckCard.Caption = "比对产品序列号";
			this.txtCheckCard.Checked = false;
			this.txtCheckCard.EditType = UserControl.EditTypes.String;
			this.txtCheckCard.Location = new System.Drawing.Point(188, 64);
			this.txtCheckCard.MaxLength = 40;
			this.txtCheckCard.Multiline = false;
			this.txtCheckCard.Name = "txtCheckCard";
			this.txtCheckCard.PasswordChar = '\0';
			this.txtCheckCard.ReadOnly = false;
			this.txtCheckCard.ShowCheckBox = false;
			this.txtCheckCard.Size = new System.Drawing.Size(299, 24);
			this.txtCheckCard.TabIndex = 4;
			this.txtCheckCard.TabNext = true;
			this.txtCheckCard.Value = "";
			this.txtCheckCard.WidthType = UserControl.WidthTypes.Long;
			this.txtCheckCard.XAlign = 287;
			this.txtCheckCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCheckCard_TxtboxKeyPress);
			// 
			// lblResult
			// 
			this.lblResult.BackColor = System.Drawing.Color.Transparent;
			this.lblResult.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblResult.Font = new System.Drawing.Font("宋体", 72F);
			this.lblResult.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lblResult.Location = new System.Drawing.Point(0, 101);
			this.lblResult.Name = "lblResult";
			this.lblResult.Size = new System.Drawing.Size(656, 368);
			this.lblResult.TabIndex = 5;
			this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FSNCheck
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(656, 469);
			this.Controls.Add(this.lblResult);
			this.Controls.Add(this.txtCheckCard);
			this.Controls.Add(this.txtRunningCard);
			this.Name = "FSNCheck";
			this.Text = "产品序列号比对";
			this.Activated += new System.EventHandler(this.FSNCheck_Activated);
			this.ResumeLayout(false);

		}
		#endregion

		private void FSNCheck_Activated(object sender, System.EventArgs e)
		{
			txtRunningCard.TextFocus(true, true);
		}

		private void txtCheckCard_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				if(txtCheckCard.Value.Trim() != String.Empty)
				{
					DislplayMessage();
					txtRunningCard.Value = String.Empty;
					txtCheckCard.Value = String.Empty;
//					txtRunningCard.TextFocus(true, true);
				}
			}
		}

		private void txtRunningCard_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
				//Amoi,Laws Lu,2005/08/02,注释
				//			}
				//			catch (Exception e)
				//			{
				//				productmessages.Add(new UserControl.Message(e));
				//			}
				//EndAmoi
				//dataCollect = null;
//				if(txtRunningCard.Value.Trim() != String.Empty)
//				{
//					txtCheckCard.TextFocus(true, true);
//				}
		}
		//Laws Lu,2006/06/12 Display Check Message
		private void DislplayMessage()
		{
			if(txtRunningCard.Value.Trim().ToUpper() == 
				txtCheckCard.Value.Trim().ToUpper() )
			{
				lblResult.Text = /*txtRunningCard.Value.Trim().ToUpper() + "\r\n" + */Pass;
				lblResult.BackColor = Color.Green;
			}
			else
			{
				lblResult.Text = /*txtRunningCard.Value.Trim().ToUpper() + "\r\n" +*/ Fail;
				lblResult.BackColor = Color.Red;
			}
		}
	}
}
