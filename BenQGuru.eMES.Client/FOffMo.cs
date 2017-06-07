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
	/// FOffMo 的摘要说明。
	/// </summary>
	public class FOffMo : System.Windows.Forms.Form
	{
		private UserControl.UCLabelEdit txtMoCode;
		private UserControl.UCLabelEdit txtPlanQty;
		private UserControl.UCLabelEdit txtInputQty;
		private UserControl.UCLabelEdit txtItemCode;
		private UserControl.UCLabelEdit txtActQty;
		private UserControl.UCLabelEdit txtScrapQty;
		private UserControl.UCLabelEdit txtOffQty;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private UserControl.UCMessage ucMessage;
		private UserControl.UCButton ucBtnExit;
		private UserControl.UCLabelEdit txtSumNum;
		private UserControl.UCLabelEdit txtRCard;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Domain.MOModel.MO MO = null;
		private UserControl.UCButton bntLock;
		private UserControl.UCButton btnProcessAll;

		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		public FOffMo()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			UserControl.UIStyleBuilder.FormUI(this);

			ucMessage.Add(">>$CS_Please_Input_MOCode");

			txtMoCode.TextFocus(false, true);
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FOffMo));
            this.txtMoCode = new UserControl.UCLabelEdit();
            this.txtPlanQty = new UserControl.UCLabelEdit();
            this.txtInputQty = new UserControl.UCLabelEdit();
            this.txtItemCode = new UserControl.UCLabelEdit();
            this.txtActQty = new UserControl.UCLabelEdit();
            this.txtScrapQty = new UserControl.UCLabelEdit();
            this.txtOffQty = new UserControl.UCLabelEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnProcessAll = new UserControl.UCButton();
            this.bntLock = new UserControl.UCButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucBtnExit = new UserControl.UCButton();
            this.txtSumNum = new UserControl.UCLabelEdit();
            this.txtRCard = new UserControl.UCLabelEdit();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ucMessage = new UserControl.UCMessage();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMoCode
            // 
            this.txtMoCode.AllowEditOnlyChecked = true;
            this.txtMoCode.Caption = "工单";
            this.txtMoCode.Checked = false;
            this.txtMoCode.EditType = UserControl.EditTypes.String;
            this.txtMoCode.Location = new System.Drawing.Point(46, 16);
            this.txtMoCode.MaxLength = 40;
            this.txtMoCode.Multiline = false;
            this.txtMoCode.Name = "txtMoCode";
            this.txtMoCode.PasswordChar = '\0';
            this.txtMoCode.ReadOnly = false;
            this.txtMoCode.ShowCheckBox = false;
            this.txtMoCode.Size = new System.Drawing.Size(204, 24);
            this.txtMoCode.TabIndex = 1;
            this.txtMoCode.TabNext = false;
            this.txtMoCode.Value = "";
            this.txtMoCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtMoCode.XAlign = 90;
            this.txtMoCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMoCode_TxtboxKeyPress);
            // 
            // txtPlanQty
            // 
            this.txtPlanQty.AllowEditOnlyChecked = true;
            this.txtPlanQty.Caption = "计划数量";
            this.txtPlanQty.Checked = false;
            this.txtPlanQty.EditType = UserControl.EditTypes.String;
            this.txtPlanQty.Location = new System.Drawing.Point(269, 56);
            this.txtPlanQty.MaxLength = 40;
            this.txtPlanQty.Multiline = false;
            this.txtPlanQty.Name = "txtPlanQty";
            this.txtPlanQty.PasswordChar = '\0';
            this.txtPlanQty.ReadOnly = true;
            this.txtPlanQty.ShowCheckBox = false;
            this.txtPlanQty.Size = new System.Drawing.Size(233, 24);
            this.txtPlanQty.TabIndex = 2;
            this.txtPlanQty.TabNext = false;
            this.txtPlanQty.Value = "";
            this.txtPlanQty.WidthType = UserControl.WidthTypes.Normal;
            this.txtPlanQty.XAlign = 342;
            // 
            // txtInputQty
            // 
            this.txtInputQty.AllowEditOnlyChecked = true;
            this.txtInputQty.Caption = "已投入数量";
            this.txtInputQty.Checked = false;
            this.txtInputQty.EditType = UserControl.EditTypes.String;
            this.txtInputQty.Location = new System.Drawing.Point(539, 56);
            this.txtInputQty.MaxLength = 40;
            this.txtInputQty.Multiline = false;
            this.txtInputQty.Name = "txtInputQty";
            this.txtInputQty.PasswordChar = '\0';
            this.txtInputQty.ReadOnly = true;
            this.txtInputQty.ShowCheckBox = false;
            this.txtInputQty.Size = new System.Drawing.Size(247, 24);
            this.txtInputQty.TabIndex = 3;
            this.txtInputQty.TabNext = false;
            this.txtInputQty.Value = "";
            this.txtInputQty.WidthType = UserControl.WidthTypes.Normal;
            this.txtInputQty.XAlign = 627;
            // 
            // txtItemCode
            // 
            this.txtItemCode.AllowEditOnlyChecked = true;
            this.txtItemCode.Caption = "产品代码";
            this.txtItemCode.Checked = false;
            this.txtItemCode.EditType = UserControl.EditTypes.String;
            this.txtItemCode.Location = new System.Drawing.Point(17, 56);
            this.txtItemCode.MaxLength = 40;
            this.txtItemCode.Multiline = false;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.PasswordChar = '\0';
            this.txtItemCode.ReadOnly = true;
            this.txtItemCode.ShowCheckBox = false;
            this.txtItemCode.Size = new System.Drawing.Size(233, 24);
            this.txtItemCode.TabIndex = 4;
            this.txtItemCode.TabNext = false;
            this.txtItemCode.Value = "";
            this.txtItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtItemCode.XAlign = 90;
            // 
            // txtActQty
            // 
            this.txtActQty.AllowEditOnlyChecked = true;
            this.txtActQty.Caption = "已完成产量";
            this.txtActQty.Checked = false;
            this.txtActQty.EditType = UserControl.EditTypes.String;
            this.txtActQty.Location = new System.Drawing.Point(258, 96);
            this.txtActQty.MaxLength = 40;
            this.txtActQty.Multiline = false;
            this.txtActQty.Name = "txtActQty";
            this.txtActQty.PasswordChar = '\0';
            this.txtActQty.ReadOnly = true;
            this.txtActQty.ShowCheckBox = false;
            this.txtActQty.Size = new System.Drawing.Size(247, 24);
            this.txtActQty.TabIndex = 5;
            this.txtActQty.TabNext = false;
            this.txtActQty.Value = "";
            this.txtActQty.WidthType = UserControl.WidthTypes.Normal;
            this.txtActQty.XAlign = 346;
            // 
            // txtScrapQty
            // 
            this.txtScrapQty.AllowEditOnlyChecked = true;
            this.txtScrapQty.Caption = "已拆解报废数量";
            this.txtScrapQty.Checked = false;
            this.txtScrapQty.EditType = UserControl.EditTypes.String;
            this.txtScrapQty.Location = new System.Drawing.Point(511, 96);
            this.txtScrapQty.MaxLength = 40;
            this.txtScrapQty.Multiline = false;
            this.txtScrapQty.Name = "txtScrapQty";
            this.txtScrapQty.PasswordChar = '\0';
            this.txtScrapQty.ReadOnly = true;
            this.txtScrapQty.ShowCheckBox = false;
            this.txtScrapQty.Size = new System.Drawing.Size(276, 24);
            this.txtScrapQty.TabIndex = 6;
            this.txtScrapQty.TabNext = false;
            this.txtScrapQty.Value = "";
            this.txtScrapQty.WidthType = UserControl.WidthTypes.Normal;
            this.txtScrapQty.XAlign = 627;
            // 
            // txtOffQty
            // 
            this.txtOffQty.AllowEditOnlyChecked = true;
            this.txtOffQty.Caption = "已脱离数量";
            this.txtOffQty.Checked = false;
            this.txtOffQty.EditType = UserControl.EditTypes.String;
            this.txtOffQty.Location = new System.Drawing.Point(3, 96);
            this.txtOffQty.MaxLength = 40;
            this.txtOffQty.Multiline = false;
            this.txtOffQty.Name = "txtOffQty";
            this.txtOffQty.PasswordChar = '\0';
            this.txtOffQty.ReadOnly = true;
            this.txtOffQty.ShowCheckBox = false;
            this.txtOffQty.Size = new System.Drawing.Size(247, 24);
            this.txtOffQty.TabIndex = 7;
            this.txtOffQty.TabNext = false;
            this.txtOffQty.Value = "";
            this.txtOffQty.WidthType = UserControl.WidthTypes.Normal;
            this.txtOffQty.XAlign = 91;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnProcessAll);
            this.panel1.Controls.Add(this.txtInputQty);
            this.panel1.Controls.Add(this.txtScrapQty);
            this.panel1.Controls.Add(this.txtMoCode);
            this.panel1.Controls.Add(this.txtActQty);
            this.panel1.Controls.Add(this.txtPlanQty);
            this.panel1.Controls.Add(this.txtItemCode);
            this.panel1.Controls.Add(this.txtOffQty);
            this.panel1.Controls.Add(this.bntLock);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(815, 136);
            this.panel1.TabIndex = 8;
            // 
            // btnProcessAll
            // 
            this.btnProcessAll.BackColor = System.Drawing.SystemColors.Control;
            this.btnProcessAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProcessAll.BackgroundImage")));
            this.btnProcessAll.ButtonType = UserControl.ButtonTypes.None;
            this.btnProcessAll.Caption = "全部脱离";
            this.btnProcessAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProcessAll.Location = new System.Drawing.Point(369, 16);
            this.btnProcessAll.Name = "btnProcessAll";
            this.btnProcessAll.Size = new System.Drawing.Size(88, 22);
            this.btnProcessAll.TabIndex = 12;
            this.btnProcessAll.TabStop = false;
            this.btnProcessAll.Click += new System.EventHandler(this.btnProcessAll_Click);
            // 
            // bntLock
            // 
            this.bntLock.BackColor = System.Drawing.SystemColors.Control;
            this.bntLock.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bntLock.BackgroundImage")));
            this.bntLock.ButtonType = UserControl.ButtonTypes.None;
            this.bntLock.Caption = "锁定";
            this.bntLock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntLock.Location = new System.Drawing.Point(273, 16);
            this.bntLock.Name = "bntLock";
            this.bntLock.Size = new System.Drawing.Size(88, 22);
            this.bntLock.TabIndex = 11;
            this.bntLock.TabStop = false;
            this.bntLock.Click += new System.EventHandler(this.bntLock_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucBtnExit);
            this.panel2.Controls.Add(this.txtSumNum);
            this.panel2.Controls.Add(this.txtRCard);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 489);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(815, 63);
            this.panel2.TabIndex = 9;
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(603, 15);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 8;
            // 
            // txtSumNum
            // 
            this.txtSumNum.AllowEditOnlyChecked = true;
            this.txtSumNum.Caption = "已采集数量";
            this.txtSumNum.Checked = false;
            this.txtSumNum.EditType = UserControl.EditTypes.String;
            this.txtSumNum.Location = new System.Drawing.Point(337, 15);
            this.txtSumNum.MaxLength = 40;
            this.txtSumNum.Multiline = false;
            this.txtSumNum.Name = "txtSumNum";
            this.txtSumNum.PasswordChar = '\0';
            this.txtSumNum.ReadOnly = true;
            this.txtSumNum.ShowCheckBox = false;
            this.txtSumNum.Size = new System.Drawing.Size(247, 24);
            this.txtSumNum.TabIndex = 7;
            this.txtSumNum.TabNext = false;
            this.txtSumNum.Value = "0";
            this.txtSumNum.WidthType = UserControl.WidthTypes.Normal;
            this.txtSumNum.XAlign = 425;
            // 
            // txtRCard
            // 
            this.txtRCard.AllowEditOnlyChecked = true;
            this.txtRCard.Caption = "产品序列号";
            this.txtRCard.Checked = false;
            this.txtRCard.EditType = UserControl.EditTypes.String;
            this.txtRCard.Location = new System.Drawing.Point(3, 15);
            this.txtRCard.MaxLength = 40;
            this.txtRCard.Multiline = false;
            this.txtRCard.Name = "txtRCard";
            this.txtRCard.PasswordChar = '\0';
            this.txtRCard.ReadOnly = false;
            this.txtRCard.ShowCheckBox = false;
            this.txtRCard.Size = new System.Drawing.Size(328, 24);
            this.txtRCard.TabIndex = 6;
            this.txtRCard.TabNext = false;
            this.txtRCard.Value = "";
            this.txtRCard.WidthType = UserControl.WidthTypes.Long;
            this.txtRCard.XAlign = 91;
            this.txtRCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRCard_TxtboxKeyPress);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ucMessage);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 136);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(815, 353);
            this.panel3.TabIndex = 10;
            // 
            // ucMessage
            // 
            this.ucMessage.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessage.ButtonVisible = false;
            this.ucMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessage.Location = new System.Drawing.Point(0, 0);
            this.ucMessage.Name = "ucMessage";
            this.ucMessage.Size = new System.Drawing.Size(815, 353);
            this.ucMessage.TabIndex = 1;
            // 
            // FOffMo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(815, 552);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FOffMo";
            this.Text = "脱离工单";
            this.Load += new System.EventHandler(this.FOffMo_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		//锁定工单输入框
		private void LockMoInput()
		{
			txtMoCode.Enabled = false;
			bntLock.Caption = "解除锁定";
		}
		//解除工单输入框锁定
		private void UnLockMoInput()
		{
			txtMoCode.Enabled = true;
			bntLock.Caption = "锁定";
		}

		private void txtMoCode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar =='\r')
			{
				Messages msg = new Messages();

				if ( txtMoCode.Value.Trim() == string.Empty )
				{
					ucMessage.Add(">>$CS_Please_Input_MOCode");

					txtMoCode.TextFocus(false, true);
				}
				else
				{
					msg.AddMessages(DisplayMoInfo(FormatHelper.CleanString(txtMoCode.Value.Trim().ToUpper())));

					if(msg.IsSuccess())
					{
						LockMoInput();

						ucMessage.Add(">>$CS_Please_Input_RunningCard");//请输入产品序列号

						txtRCard.TextFocus(false, true);
					}
					else
					{
						ClearMoInfo();

						ucMessage.Add(msg);

						txtMoCode.TextFocus(false, true);
					}
				}
			}		
		}

		#region 更新显示工单信息
		//从数据库获取工单
		private object GetMo(string moCode)
		{
//			if(listActionCheckStatus.Contains(moCode))
//			{
//				return ((ActionCheckStatus)listActionCheckStatus[moCode]).MO;
//			}
//			else
//			{
//			if(MO == null || (MO != null && MO.MOCode  != moCode))
//			{
			MO =  (new MOFacade(DataProvider)).GetMO(moCode) as Domain.MOModel.MO;
//			}
			return MO;
//			}
		}

		//清除工单显示数据
		private void ClearMoInfo()
		{
			txtMoCode.Value = String.Empty;
			txtItemCode.Value =  String.Empty;
			txtInputQty.Value = String.Empty;
			txtPlanQty.Value  = String.Empty;
			txtScrapQty.Value = String.Empty;
			txtActQty.Value  = String.Empty;
			txtOffQty.Value = String.Empty;
		}
		//设置工单显示值
		private void SetMoValue(Domain.MOModel.MO mo)
		{
			txtMoCode.Value = mo.MOCode;
			txtItemCode.Value = mo.ItemCode;
			txtInputQty.Value =  Convert.ToString(Convert.ToInt32(mo.MOInputQty));
			txtPlanQty.Value  =  Convert.ToString(Convert.ToInt32( mo.MOPlanQty));
			txtScrapQty.Value =  Convert.ToString(Convert.ToInt32(mo.MOScrapQty));
			txtActQty.Value =  Convert.ToString(Convert.ToInt32(mo.MOActualQty));
			txtOffQty.Value =  Convert.ToString(Convert.ToInt32(mo.MOOffQty));
		}
		//显示工单信息
		private Messages DisplayMoInfo(string moCode)
		{
			Messages msg = new Messages();

			object obj = GetMo(moCode);
			
			if ( obj == null )
			{
				msg.Add(new UserControl.Message(MessageType.Error,"$CS_MO_Not_Exist $Domain_MO=" + moCode));
			}
			else
			{
				Domain.MOModel.MO mo = obj as Domain.MOModel.MO;

				ClearMoInfo();

				if ( mo.MOStatus != Web.Helper.MOManufactureStatus.MOSTATUS_RELEASE && 
					mo.MOStatus !=  Web.Helper.MOManufactureStatus.MOSTATUS_OPEN)
				{				
					msg.Add(new UserControl.Message(MessageType.Error,">>$Error_CS_MO_Should_be_Release_or_Open"));
				}
				else
				{
					SetMoValue(mo);
				}
			}

			return msg;
		}
		#endregion
		//更新采集数量
		private void UpdateCollectQty()
		{
			txtSumNum.Value = Convert.ToString(Convert.ToInt32(FormatHelper.CleanString(txtSumNum.Value.Trim())) 
				+ 1);
		}

		private void txtRCard_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar =='\r')
			{
				OffMo();
			}
		}
		
		private Messages DoAction(string runningCard,string moCode)
		{
			#region 脱离工单业务处理
			Messages msg = new Messages();

            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(runningCard.Trim().ToUpper(), string.Empty);

			ActionOnLineHelper _helper = new ActionOnLineHelper(DataProvider);
            msg = _helper.GetIDInfo(sourceRCard);

			if ( msg.IsSuccess() )
			{
				ProductInfo product = (ProductInfo)msg.GetData().Values[0];

				#region	必要的检查
				if(product == null || (product != null && product.LastSimulation == null))
				{
					msg.Add(new UserControl.Message(MessageType.Error ,"$NoSimulation"));
				}
				else if(product != null && product.LastSimulation.MOCode != moCode)
				{
					msg.Add(new UserControl.Message(MessageType.Error,"$CS_MO_NOT_MATCH $MOCode=" + product.LastSimulation.MOCode));
				}
				#endregion

				if(msg.IsSuccess())
				{
					OffMoEventArgs args = new OffMoEventArgs( 
						ActionType.DataCollectAction_OffMo,
                        sourceRCard.Trim(), 
						ApplicationService.Current().UserCode,
						ApplicationService.Current().ResourceCode,
						product, 
						moCode);

					args.MOType = (GetMo(moCode) as Domain.MOModel.MO).MOType;

					IAction action = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_OffMo);

					//Laws Lu,2005/10/19,新增	缓解性能问题
					//					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
					//					if(!((SQLDomainDataProvider)DataProvider).PersistBroker.IsInTransaction)
					//					{
					DataProvider.BeginTransaction();
					//					}
					try
					{
						msg.AddMessages(action.Execute(args));	

						if ( msg.IsSuccess() )
						{
							//							if(!((SQLDomainDataProvider)DataProvider).PersistBroker.IsInTransaction)
							//							{
							DataProvider.CommitTransaction();					
							//							}
							msg.Add( new UserControl.Message(MessageType.Success,string.Format("$CS_OffMO_Success $MOCode={0} $CS_Param_ID={1}", moCode,runningCard)) );

							DisplayMoInfo(moCode);
							UpdateCollectQty();
						}
						else
						{
							//							if(!((SQLDomainDataProvider)DataProvider).PersistBroker.IsInTransaction)
							//							{
							this.DataProvider.RollbackTransaction();
							//							}
						}
					}
					catch(Exception ex)
					{
						//						if(!((SQLDomainDataProvider)DataProvider).PersistBroker.IsInTransaction)
						//						{
						DataProvider.RollbackTransaction();
						//						}
						msg.Add(new UserControl.Message(ex));
					}
					finally
					{
						//Laws Lu,2005/10/19,新增	缓解性能问题
						//						if(!((SQLDomainDataProvider)DataProvider).PersistBroker.IsInTransaction)
						//						{
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
						//						}
					}
				}
			}
			#endregion

			return msg;
		}

		private void OffMo()
		{
			Messages msg = new Messages();

			#region Check
			if ( txtMoCode.Value.Trim() == string.Empty )
			{
				ucMessage.Add(">>$CS_Please_Input_MOCode");

				txtMoCode.TextFocus(false, true);

				return;
			}

			if ( txtRCard.Value.Trim() == string.Empty )
			{
				ucMessage.Add(">>$CS_Please_Input_RunningCard");

				txtRCard.TextFocus(false, true);

				return;
			}
			#endregion

			string runningCard = txtRCard.Value.Trim().ToUpper();
			string moCode = txtMoCode.Value.Trim().ToUpper();
			
			msg.AddMessages(DoAction(runningCard,moCode));

			#region 信息显示和焦点设置

			ucMessage.Add( msg );
			ucMessage.Add(">>$CS_Please_Input_RunningCard");

			txtRCard.Value = String.Empty;
			txtRCard.TextFocus(false, true);

			#endregion
		}

		private void bntLock_Click(object sender, System.EventArgs e)
		{
			if(bntLock.Caption == "锁定")
			{
				LockMoInput();
			}
			else
			{
				UnLockMoInput();
			}
		}

		private Messages AllOffMO(string rcard,string mocode,int idMerge)
		{
			Messages msg = new Messages();
			try
			{
				//更新产品状态
				DataProvider.CustomExecute(new SQLCondition("UPDATE TBLSIMULATION SET ISCOM = '1'"
					+ ",EATTRIBUTE1 = '" + ProductStatus.OffMo + "'"
					+ ",PRODUCTSTATUS = '" + ProductStatus.OffMo + "'"
					+ " WHERE RCARD = '" + rcard + "'" 
					+ " AND MOCODE = '" + mocode + "'"));
				//更新工单数量
				DataProvider.CustomExecute(new SQLCondition("UPDATE TBLMO SET OFFMOQTY = OFFMOQTY + " + idMerge
					+ " WHERE MOCODE = '" + mocode + "'"));
			}
			catch(Exception ex)
			{
				msg.Add(new UserControl.Message(ex));
				throw ex;
			}
			return msg;
		}

		private void btnProcessAll_Click(object sender, System.EventArgs e)
		{
			object[] objSims = (new DataCollectFacade(DataProvider)).GetOnlineSimulationByMoCode(txtMoCode.Value.Trim());

			if(objSims != null && objSims.Length > 0)
			{
				DataProvider.BeginTransaction();
				Messages msg = new Messages();
				try
				{
					foreach(Simulation sim in objSims)
					{
						if(!msg.IsSuccess())
						{
							break;
						}
						msg.AddMessages(AllOffMO(sim.RunningCard,sim.MOCode,Convert.ToInt32(sim.IDMergeRule)));
					}

					MOFacade moFac = new MOFacade(DataProvider);
					object objMO = moFac.GetMO(txtMoCode.Value.Trim());

					if(objMO != null)
					{
						MO mo = objMO as MO;
						mo.MOStatus = Web.Helper.MOManufactureStatus.MOSTATUS_CLOSE;

						//Laws Lu,2006/11/13 uniform system collect date
						DBDateTime dbDateTime;
						
						dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
							

						mo.MOActualEndDate = dbDateTime.DBDate;
						moFac.MOStatusChanged(mo);
					}

					if(msg.IsSuccess())
					{
						DataProvider.CommitTransaction();
					}
					else
					{
						DataProvider.RollbackTransaction();
					}

				}
				catch(Exception ex)
				{
					Log.Error(ex.Message);

					msg.Add(new UserControl.Message(ex));
					DataProvider.RollbackTransaction();
				}
				finally
				{
					((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				}

				ucMessage.Add( msg );
				ucMessage.Add(">>$CS_Please_Input_RunningCard");

				txtRCard.Value = String.Empty;
				txtRCard.TextFocus(false, true);
			}
		}

		private void FOffMo_Load(object sender, System.EventArgs e)
		{
			
		}

	}
}
