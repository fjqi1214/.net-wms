using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FCollectMo 的摘要说明。
	/// </summary>
	public class FCollectMo : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private UserControl.UCLabelEdit ucLEMOCode;
		private UserControl.UCLabelEdit ucLEItemCode;
		private UserControl.UCLabelEdit ucLEInputQty;
		private UserControl.UCLabelEdit ucLEPlanQty;
		private UserControl.UCLabelEdit ucLEScrapQty;
		private UserControl.UCLabelEdit ucLENeedQty;
		private UserControl.UCLabelEdit ucLERunningCard;
		private UserControl.UCButton ucBtnExit;
		private UserControl.UCButton ucBtnOK;
		private UserControl.UCMessage ucMessage;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		private string _moCode = string.Empty;
		private ActionOnLineHelper _helper = null;
		private MOFacade _moFacade = null;

		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		public FCollectMo()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			UserControl.UIStyleBuilder.FormUI(this);

			this.ucMessage.Add(">>$CS_Please_Input_MOCode");//请输入工单代码
			this.ucLEMOCode.TextFocus(false, true);

			this._moFacade = new MOFacade( this.DataProvider );
			this._helper = new ActionOnLineHelper( this.DataProvider );
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCollectMo));
            this.ucLEMOCode = new UserControl.UCLabelEdit();
            this.ucLEItemCode = new UserControl.UCLabelEdit();
            this.ucLEPlanQty = new UserControl.UCLabelEdit();
            this.ucLEInputQty = new UserControl.UCLabelEdit();
            this.ucLEScrapQty = new UserControl.UCLabelEdit();
            this.ucLENeedQty = new UserControl.UCLabelEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucLERunningCard = new UserControl.UCLabelEdit();
            this.ucBtnExit = new UserControl.UCButton();
            this.ucBtnOK = new UserControl.UCButton();
            this.ucMessage = new UserControl.UCMessage();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucLEMOCode
            // 
            this.ucLEMOCode.AllowEditOnlyChecked = true;
            this.ucLEMOCode.Caption = "工单";
            this.ucLEMOCode.Checked = false;
            this.ucLEMOCode.EditType = UserControl.EditTypes.String;
            this.ucLEMOCode.Location = new System.Drawing.Point(63, 16);
            this.ucLEMOCode.MaxLength = 40;
            this.ucLEMOCode.Multiline = false;
            this.ucLEMOCode.Name = "ucLEMOCode";
            this.ucLEMOCode.PasswordChar = '\0';
            this.ucLEMOCode.ReadOnly = false;
            this.ucLEMOCode.ShowCheckBox = false;
            this.ucLEMOCode.Size = new System.Drawing.Size(170, 24);
            this.ucLEMOCode.TabIndex = 0;
            this.ucLEMOCode.TabNext = false;
            this.ucLEMOCode.Value = "";
            this.ucLEMOCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEMOCode.XAlign = 100;
            this.ucLEMOCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEMOCode_TxtboxKeyPress);
            // 
            // ucLEItemCode
            // 
            this.ucLEItemCode.AllowEditOnlyChecked = true;
            this.ucLEItemCode.Caption = "产品代码";
            this.ucLEItemCode.Checked = false;
            this.ucLEItemCode.EditType = UserControl.EditTypes.String;
            this.ucLEItemCode.Location = new System.Drawing.Point(39, 48);
            this.ucLEItemCode.MaxLength = 40;
            this.ucLEItemCode.Multiline = false;
            this.ucLEItemCode.Name = "ucLEItemCode";
            this.ucLEItemCode.PasswordChar = '\0';
            this.ucLEItemCode.ReadOnly = true;
            this.ucLEItemCode.ShowCheckBox = false;
            this.ucLEItemCode.Size = new System.Drawing.Size(194, 24);
            this.ucLEItemCode.TabIndex = 1;
            this.ucLEItemCode.TabNext = false;
            this.ucLEItemCode.Value = "";
            this.ucLEItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemCode.XAlign = 100;
            // 
            // ucLEPlanQty
            // 
            this.ucLEPlanQty.AllowEditOnlyChecked = true;
            this.ucLEPlanQty.Caption = "计划产量";
            this.ucLEPlanQty.Checked = false;
            this.ucLEPlanQty.EditType = UserControl.EditTypes.String;
            this.ucLEPlanQty.Location = new System.Drawing.Point(336, 48);
            this.ucLEPlanQty.MaxLength = 40;
            this.ucLEPlanQty.Multiline = false;
            this.ucLEPlanQty.Name = "ucLEPlanQty";
            this.ucLEPlanQty.PasswordChar = '\0';
            this.ucLEPlanQty.ReadOnly = true;
            this.ucLEPlanQty.ShowCheckBox = false;
            this.ucLEPlanQty.Size = new System.Drawing.Size(194, 24);
            this.ucLEPlanQty.TabIndex = 4;
            this.ucLEPlanQty.TabNext = false;
            this.ucLEPlanQty.Value = "";
            this.ucLEPlanQty.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEPlanQty.XAlign = 397;
            // 
            // ucLEInputQty
            // 
            this.ucLEInputQty.AllowEditOnlyChecked = true;
            this.ucLEInputQty.Caption = "已投入产量";
            this.ucLEInputQty.Checked = false;
            this.ucLEInputQty.EditType = UserControl.EditTypes.String;
            this.ucLEInputQty.Location = new System.Drawing.Point(27, 80);
            this.ucLEInputQty.MaxLength = 40;
            this.ucLEInputQty.Multiline = false;
            this.ucLEInputQty.Name = "ucLEInputQty";
            this.ucLEInputQty.PasswordChar = '\0';
            this.ucLEInputQty.ReadOnly = true;
            this.ucLEInputQty.ShowCheckBox = false;
            this.ucLEInputQty.Size = new System.Drawing.Size(206, 24);
            this.ucLEInputQty.TabIndex = 2;
            this.ucLEInputQty.TabNext = false;
            this.ucLEInputQty.Value = "";
            this.ucLEInputQty.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEInputQty.XAlign = 100;
            // 
            // ucLEScrapQty
            // 
            this.ucLEScrapQty.AllowEditOnlyChecked = true;
            this.ucLEScrapQty.Caption = "已拆解、报废量";
            this.ucLEScrapQty.Checked = false;
            this.ucLEScrapQty.EditType = UserControl.EditTypes.String;
            this.ucLEScrapQty.Location = new System.Drawing.Point(299, 80);
            this.ucLEScrapQty.MaxLength = 40;
            this.ucLEScrapQty.Multiline = false;
            this.ucLEScrapQty.Name = "ucLEScrapQty";
            this.ucLEScrapQty.PasswordChar = '\0';
            this.ucLEScrapQty.ReadOnly = true;
            this.ucLEScrapQty.ShowCheckBox = false;
            this.ucLEScrapQty.Size = new System.Drawing.Size(230, 24);
            this.ucLEScrapQty.TabIndex = 5;
            this.ucLEScrapQty.TabNext = false;
            this.ucLEScrapQty.Value = "";
            this.ucLEScrapQty.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEScrapQty.XAlign = 396;
            // 
            // ucLENeedQty
            // 
            this.ucLENeedQty.AllowEditOnlyChecked = true;
            this.ucLENeedQty.Caption = "未投入产量";
            this.ucLENeedQty.Checked = false;
            this.ucLENeedQty.EditType = UserControl.EditTypes.String;
            this.ucLENeedQty.Location = new System.Drawing.Point(324, 16);
            this.ucLENeedQty.MaxLength = 40;
            this.ucLENeedQty.Multiline = false;
            this.ucLENeedQty.Name = "ucLENeedQty";
            this.ucLENeedQty.PasswordChar = '\0';
            this.ucLENeedQty.ReadOnly = true;
            this.ucLENeedQty.ShowCheckBox = false;
            this.ucLENeedQty.Size = new System.Drawing.Size(206, 24);
            this.ucLENeedQty.TabIndex = 3;
            this.ucLENeedQty.TabNext = false;
            this.ucLENeedQty.Value = "";
            this.ucLENeedQty.WidthType = UserControl.WidthTypes.Normal;
            this.ucLENeedQty.XAlign = 397;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucLERunningCard);
            this.groupBox1.Controls.Add(this.ucBtnExit);
            this.groupBox1.Controls.Add(this.ucBtnOK);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 255);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(543, 51);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // ucLERunningCard
            // 
            this.ucLERunningCard.AllowEditOnlyChecked = true;
            this.ucLERunningCard.Caption = "输入框";
            this.ucLERunningCard.Checked = false;
            this.ucLERunningCard.EditType = UserControl.EditTypes.String;
            this.ucLERunningCard.Location = new System.Drawing.Point(12, 18);
            this.ucLERunningCard.MaxLength = 40;
            this.ucLERunningCard.Multiline = false;
            this.ucLERunningCard.Name = "ucLERunningCard";
            this.ucLERunningCard.PasswordChar = '\0';
            this.ucLERunningCard.ReadOnly = false;
            this.ucLERunningCard.ShowCheckBox = false;
            this.ucLERunningCard.Size = new System.Drawing.Size(249, 24);
            this.ucLERunningCard.TabIndex = 0;
            this.ucLERunningCard.TabNext = false;
            this.ucLERunningCard.Value = "";
            this.ucLERunningCard.WidthType = UserControl.WidthTypes.Long;
            this.ucLERunningCard.XAlign = 61;
            this.ucLERunningCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLERunningCard_TxtboxKeyPress);
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(422, 17);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 2;
            // 
            // ucBtnOK
            // 
            this.ucBtnOK.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnOK.BackgroundImage")));
            this.ucBtnOK.ButtonType = UserControl.ButtonTypes.Confirm;
            this.ucBtnOK.Caption = "确认";
            this.ucBtnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnOK.Location = new System.Drawing.Point(318, 17);
            this.ucBtnOK.Name = "ucBtnOK";
            this.ucBtnOK.Size = new System.Drawing.Size(88, 22);
            this.ucBtnOK.TabIndex = 1;
            this.ucBtnOK.Click += new System.EventHandler(this.ucBtnOK_Click);
            // 
            // ucMessage
            // 
            this.ucMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMessage.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessage.ButtonVisible = false;
            this.ucMessage.Location = new System.Drawing.Point(4, 120);
            this.ucMessage.Name = "ucMessage";
            this.ucMessage.Size = new System.Drawing.Size(534, 133);
            this.ucMessage.TabIndex = 0;
            // 
            // FCollectMo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(543, 306);
            this.Controls.Add(this.ucMessage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ucLENeedQty);
            this.Controls.Add(this.ucLEScrapQty);
            this.Controls.Add(this.ucLEInputQty);
            this.Controls.Add(this.ucLEPlanQty);
            this.Controls.Add(this.ucLEItemCode);
            this.Controls.Add(this.ucLEMOCode);
            this.Name = "FCollectMo";
            this.Text = "归属工单采集";
            this.Closed += new System.EventHandler(this.FCollectMo_Closed);
            this.Activated += new System.EventHandler(this.FCollectMo_Activated);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void ucLEMOCode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar =='\r')
			{
				if ( this.ucLEMOCode.Value.Trim() == string.Empty )
				{
					return;
				}

				this.ucMessage.Add("<< " + this.ucLEMOCode.Value.Trim() );

				if ( this.displayMOInfo() )
				{
					this.ucMessage.Add(">>$CS_Please_Input_RunningCard");//请输入产品序列号

					this.ucLERunningCard.TextFocus(false, true);

					return;
				}
				else
				{
					this.ucMessage.Add(">>$CS_Please_Input_MOCode");

					ucLEMOCode.TextFocus(false, true);

					return;
				}
			}		
		}

		private object GetMo(string moCode)
		{
			if(listActionCheckStatus.Contains(moCode))
			{
				return ((ActionCheckStatus)listActionCheckStatus[moCode]).MO;
			}
			else
			{
				return this._moFacade.GetMO(moCode);
			}
		}

		private bool displayMOInfo()
		{	
			object obj = GetMo(this.ucLEMOCode.Value.Trim().ToUpper());
			
			if ( obj == null )
			{
				this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_MO_Not_Exist"));
				return false;
			}

			if ( (((MO)obj).MOStatus != Web.Helper.MOManufactureStatus.MOSTATUS_RELEASE) && 
				(((MO)obj).MOStatus !=  Web.Helper.MOManufactureStatus.MOSTATUS_OPEN) )
			{				
				this._moCode = string.Empty;
				this.ucLEMOCode.Value = "";
				this.ucLEItemCode.Value =  "";
				this.ucLEInputQty.Value = "";
				this.ucLEPlanQty.Value  = "";
				this.ucLEScrapQty.Value = "";
				this.ucLENeedQty.Value  = "";

				this.ucMessage.Add(new UserControl.Message(MessageType.Error,">>$Error_CS_MO_Should_be_Release_or_Open"));
				return false;
			}

			this._moCode = ((MO)obj).MOCode;
			this.ucLEItemCode.Value = ((MO)obj).ItemCode;
			this.ucLEInputQty.Value = ((MO)obj).MOInputQty.ToString();
			this.ucLEPlanQty.Value  = ((MO)obj).MOPlanQty.ToString();
			this.ucLEScrapQty.Value = ((MO)obj).MOScrapQty.ToString();
			this.ucLENeedQty.Value  = (((MO)obj).MOPlanQty - ((MO)obj).MOInputQty + ((MO)obj).MOScrapQty).ToString();
	
			
			return true;
		}

		private void ucLERunningCard_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar =='\r')
			{
				this.ucBtnOK_Click( sender, null );
			}
		}

		private Hashtable listActionCheckStatus = new Hashtable();
		private Domain.BaseSetting.Resource Resource;

		private void ucBtnOK_Click(object sender, System.EventArgs e)
		{
			if ( this.ucLEMOCode.Value.Trim() == string.Empty )
			{
				this.ucMessage.Add(">>$CS_Please_Input_MOCode");

				ucLEMOCode.TextFocus(false, true);
				return;
			}

			if ( this.ucLERunningCard.Value.Trim() == string.Empty )
			{
				this.ucMessage.Add(">>$CS_Please_Input_RunningCard");


				return;
			}

			if ( this._moCode == string.Empty )
			{
				if ( !this.displayMOInfo() )
				{
					return;
				}
			}

			string runningCard = this.ucLERunningCard.Value.Trim().ToUpper();
			this.ucMessage.Add("<< " + runningCard );
			
			Messages messages = this._helper.GetIDInfo(runningCard);

			if ( messages.IsSuccess() )
			{
				ProductInfo product= (ProductInfo)messages.GetData().Values[0];

				// Added by Icyer 2005/10/28
				if (Resource == null)
				{
					BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel=new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
					Resource = (Domain.BaseSetting.Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);
				}
				// Added end

				ActionCheckStatus actionCheckStatus = new ActionCheckStatus();

				if (listActionCheckStatus.ContainsKey(_moCode))
				{
					actionCheckStatus = (ActionCheckStatus)listActionCheckStatus[_moCode];
					actionCheckStatus.ProductInfo = null;
					actionCheckStatus.ActionList = new ArrayList();
				}
				else
				{
					listActionCheckStatus.Add(_moCode, actionCheckStatus);
				}

				GoToMOActionEventArgs args = new GoToMOActionEventArgs( 
																		ActionType.DataCollectAction_GoMO, 
																		runningCard, 
																		ApplicationService.Current().UserCode,
																		ApplicationService.Current().ResourceCode,
																		product, 
																		this._moCode);

				IAction action = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_GoMO);

				//Laws Lu,2005/10/19,新增	缓解性能问题
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
				DataProvider.BeginTransaction();
				try
				{
					messages.AddMessages(((IActionWithStatus)action).Execute(args,actionCheckStatus));	

					if ( messages.IsSuccess() )
					{
						this.DataProvider.CommitTransaction();					
						messages.Add( new UserControl.Message(MessageType.Success,string.Format("$CS_GOMO_CollectSuccess $MOCode={0}", this._moCode)) );

						actionCheckStatus.MO.MOInputQty += actionCheckStatus.MO.IDMergeRule;	
						

						this.displayMOInfo();
						
					}
					else
					{
						this.DataProvider.RollbackTransaction();
					}
				}
				catch(Exception ex)
				{
					this.DataProvider.RollbackTransaction();

					messages.Add(new UserControl.Message(ex));
				}
				finally
				{
					//Laws Lu,2005/10/19,新增	缓解性能问题
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				}

			}

			this.ucMessage.Add( messages );
			this.ucMessage.Add(">>$CS_Please_Input_RunningCard");

			this.ucLERunningCard.Value = "";
			this.ucLERunningCard.TextFocus(false, true);
		}

		private void FCollectMo_Closed(object sender, System.EventArgs e)
		{
			if (this.DataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();  
		}

		private void FCollectMo_Activated(object sender, System.EventArgs e)
		{
			ucLEMOCode.TextFocus(false, true);
		}
	}
}
