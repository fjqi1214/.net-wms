using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using UserControl;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FCollectionSoftware 的摘要说明。
	/// </summary>
	public class FCollectionSoftware : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private UserControl.UCLabelEdit ucLEInput;
		private UserControl.UCButton ucBtnOK;
		private UserControl.UCButton ucBtnRecede;
		private UserControl.UCButton ucBtnExit;
		private System.Windows.Forms.GroupBox gpbInput;
		private System.Windows.Forms.GroupBox grbCheck;
		private UserControl.UCMessage ucMessage;
		private System.Windows.Forms.CheckBox chbSoftName;

		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		private ArrayList inputBuffer = new ArrayList(3);
		private System.Windows.Forms.CheckBox chkAutoGetVersion;
		private System.Windows.Forms.CheckBox chkConfig;
		private ActionOnLineHelper _helper = null;

		public FCollectionSoftware()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//


			UserControl.UIStyleBuilder.FormUI(this);	
			this._helper = new ActionOnLineHelper( this.DataProvider );

			this.showInputHint();
			this.ucLEInput.TextFocus(false, true);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCollectionSoftware));
            this.gpbInput = new System.Windows.Forms.GroupBox();
            this.ucBtnRecede = new UserControl.UCButton();
            this.ucLEInput = new UserControl.UCLabelEdit();
            this.ucBtnExit = new UserControl.UCButton();
            this.ucBtnOK = new UserControl.UCButton();
            this.grbCheck = new System.Windows.Forms.GroupBox();
            this.chkConfig = new System.Windows.Forms.CheckBox();
            this.chkAutoGetVersion = new System.Windows.Forms.CheckBox();
            this.chbSoftName = new System.Windows.Forms.CheckBox();
            this.ucMessage = new UserControl.UCMessage();
            this.gpbInput.SuspendLayout();
            this.grbCheck.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbInput
            // 
            this.gpbInput.Controls.Add(this.ucBtnRecede);
            this.gpbInput.Controls.Add(this.ucLEInput);
            this.gpbInput.Controls.Add(this.ucBtnExit);
            this.gpbInput.Controls.Add(this.ucBtnOK);
            this.gpbInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gpbInput.Location = new System.Drawing.Point(0, 503);
            this.gpbInput.Name = "gpbInput";
            this.gpbInput.Size = new System.Drawing.Size(714, 44);
            this.gpbInput.TabIndex = 0;
            this.gpbInput.TabStop = false;
            // 
            // ucBtnRecede
            // 
            this.ucBtnRecede.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnRecede.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnRecede.BackgroundImage")));
            this.ucBtnRecede.ButtonType = UserControl.ButtonTypes.Change;
            this.ucBtnRecede.Caption = "更正";
            this.ucBtnRecede.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnRecede.Location = new System.Drawing.Point(409, 14);
            this.ucBtnRecede.Name = "ucBtnRecede";
            this.ucBtnRecede.Size = new System.Drawing.Size(88, 22);
            this.ucBtnRecede.TabIndex = 12;
            this.ucBtnRecede.Click += new System.EventHandler(this.ucBtnRecede_Click);
            // 
            // ucLEInput
            // 
            this.ucLEInput.AllowEditOnlyChecked = true;
            this.ucLEInput.Caption = "输入框";
            this.ucLEInput.Checked = false;
            this.ucLEInput.EditType = UserControl.EditTypes.String;
            this.ucLEInput.Location = new System.Drawing.Point(28, 14);
            this.ucLEInput.MaxLength = 40;
            this.ucLEInput.Multiline = false;
            this.ucLEInput.Name = "ucLEInput";
            this.ucLEInput.PasswordChar = '\0';
            this.ucLEInput.ReadOnly = false;
            this.ucLEInput.ShowCheckBox = false;
            this.ucLEInput.Size = new System.Drawing.Size(249, 24);
            this.ucLEInput.TabIndex = 0;
            this.ucLEInput.TabNext = false;
            this.ucLEInput.Value = "";
            this.ucLEInput.WidthType = UserControl.WidthTypes.Long;
            this.ucLEInput.XAlign = 77;
            this.ucLEInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEInput_TxtboxKeyPress);
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(519, 14);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 9;
            // 
            // ucBtnOK
            // 
            this.ucBtnOK.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnOK.BackgroundImage")));
            this.ucBtnOK.ButtonType = UserControl.ButtonTypes.Confirm;
            this.ucBtnOK.Caption = "确认";
            this.ucBtnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnOK.Location = new System.Drawing.Point(300, 14);
            this.ucBtnOK.Name = "ucBtnOK";
            this.ucBtnOK.Size = new System.Drawing.Size(88, 22);
            this.ucBtnOK.TabIndex = 8;
            this.ucBtnOK.Click += new System.EventHandler(this.ucBtnOK_Click);
            // 
            // grbCheck
            // 
            this.grbCheck.Controls.Add(this.chkConfig);
            this.grbCheck.Controls.Add(this.chkAutoGetVersion);
            this.grbCheck.Controls.Add(this.chbSoftName);
            this.grbCheck.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbCheck.Location = new System.Drawing.Point(0, 0);
            this.grbCheck.Name = "grbCheck";
            this.grbCheck.Size = new System.Drawing.Size(714, 37);
            this.grbCheck.TabIndex = 169;
            this.grbCheck.TabStop = false;
            // 
            // chkConfig
            // 
            this.chkConfig.Location = new System.Drawing.Point(303, 9);
            this.chkConfig.Name = "chkConfig";
            this.chkConfig.Size = new System.Drawing.Size(85, 24);
            this.chkConfig.TabIndex = 0;
            this.chkConfig.Text = "配置比对";
            // 
            // chkAutoGetVersion
            // 
            this.chkAutoGetVersion.Location = new System.Drawing.Point(155, 9);
            this.chkAutoGetVersion.Name = "chkAutoGetVersion";
            this.chkAutoGetVersion.Size = new System.Drawing.Size(139, 24);
            this.chkAutoGetVersion.TabIndex = 0;
            this.chkAutoGetVersion.Text = "自动获取软件信息";
            // 
            // chbSoftName
            // 
            this.chbSoftName.Location = new System.Drawing.Point(28, 9);
            this.chbSoftName.Name = "chbSoftName";
            this.chbSoftName.Size = new System.Drawing.Size(107, 24);
            this.chbSoftName.TabIndex = 0;
            this.chbSoftName.Text = "采集软件名称";
            // 
            // ucMessage
            // 
            this.ucMessage.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessage.ButtonVisible = false;
            this.ucMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessage.Location = new System.Drawing.Point(0, 37);
            this.ucMessage.Name = "ucMessage";
            this.ucMessage.Size = new System.Drawing.Size(714, 466);
            this.ucMessage.TabIndex = 170;
            // 
            // FCollectionSoftware
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(714, 547);
            this.Controls.Add(this.ucMessage);
            this.Controls.Add(this.grbCheck);
            this.Controls.Add(this.gpbInput);
            this.Name = "FCollectionSoftware";
            this.Text = "软件信息采集";
            this.Closed += new System.EventHandler(this.FCollectionSoftware_Closed);
            this.gpbInput.ResumeLayout(false);
            this.grbCheck.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void FCollectionSoftware_Closed(object sender, System.EventArgs e)
		{
			if (this.DataProvider != null)
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();  
			}
		}

		private void ucLEInput_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar == '\r')
			{
				this.ucBtnOK_Click( sender, null );

				//Laws Lu,2005/08/11,新增焦点设置,注释
				//SendKeys.Send("{TAB}");
			}
		}

		private void ucBtnOK_Click(object sender, System.EventArgs e)
		{
			if ( this.ucLEInput.Value.Trim() == string.Empty )
			{
				//Laws Lu,2005/08/11,新增焦点设置
				ucLEInput.TextFocus(false, true);

				return;
			}

			this.ucMessage.Add( string.Format("<<{0}",this.ucLEInput.Value.Trim()) );

			if ( this.inputBuffer.Count == 0 )		// 输入软件产品序列号
			{
				this.inputBuffer.Add( this.ucLEInput.Value.Trim().ToUpper() );

				if ( !this.chbSoftName.Checked )
				{
					this.inputBuffer.Add( string.Empty );
				}
				else
				{
					ucMessage.Add(new UserControl.Message(">>$CS_CMPleaseInputSoftName"));
					ucLEInput.Value = String.Empty;
					ucLEInput.TextFocus(false, true);
				}
			}
			else if ( this.inputBuffer.Count == 1 )			// 输入软件名称
			{
				this.inputBuffer.Add( this.ucLEInput.Value.Trim() );
				
			}		
			else if ( this.inputBuffer.Count == 2 )			// 输入软件版本
			{
				Collect();
			}

			if(chkAutoGetVersion.Checked == false)
			{
				this.ucLEInput.Value = "";
				this.showInputHint();

				//Laws Lu,2005/08/11,新增焦点设置
				ucLEInput.TextFocus(false, true);
			}
			else if(this.inputBuffer.Count == 2 && chkAutoGetVersion.Checked == true)
			{
				Collect();
			}
		}

		private void Collect()
		{
			object objVersion = null;
			if(chkAutoGetVersion.Checked == true)/*获取版本信息*/
			{
				DataCollect.DataCollectFacade dcf =new DataCollectFacade(DataProvider);

                //根据当前序列号获取产品的原始的序列号
                string sourceRCard = dcf.GetSourceCard((Convert.ToString(this.inputBuffer[0])).Trim().ToUpper(), string.Empty);

                objVersion = dcf.GetVersionCollect(sourceRCard);

				if(objVersion == null || (objVersion != null && (objVersion as Domain.DataCollect.VersionCollect).VersionInfo == String.Empty))
				{
					ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_SOFTWAREVERSION_EMPTY"));

					Domain.DataCollect.VersionError ve = new BenQGuru.eMES.Domain.DataCollect.VersionError();

					this.inputBuffer.Clear();

					this.ucLEInput.Value = "";
					this.showInputHint();
					ucLEInput.TextFocus(false, true);

					return ;
				}
			}
			this.inputBuffer.Add( this.ucLEInput.Value.Trim().ToUpper() );

			// 存库
			this.doAction(objVersion );

			this.inputBuffer.Clear();
		}

		private void ucBtnRecede_Click(object sender, System.EventArgs e)
		{
			if ( this.inputBuffer.Count > 0 )
			{
				// 删除不必输的软件名称
				if ( this.inputBuffer[this.inputBuffer.Count-1].ToString() == string.Empty )
				{
					this.inputBuffer.RemoveAt( this.inputBuffer.Count-1 );
				}

				this.inputBuffer.RemoveAt( this.inputBuffer.Count-1 );

				this.showInputHint();
			}

			//Laws Lu,2005/08/11,新增焦点设置
			ucLEInput.TextFocus(false, true);
		}

		private void showInputHint()
		{
			if ( this.inputBuffer.Count == 0 )				// 输入软件产品序列号
			{		
				this.ucMessage.Add(">>$CS_Please_Input_Software_Product_ID");	
				this.chbSoftName.Enabled = true;	
			}
			else if ( this.inputBuffer.Count == 1 && chbSoftName.Checked != true)			//输入软件名称
			{
				this.ucMessage.Add(">>$CS_Please_Input_Software_Name");	
				this.chbSoftName.Enabled = false;	
			}
			else if ( this.inputBuffer.Count == 2 && !chkAutoGetVersion.Checked )			// 输入软件版本
			{	
				this.ucMessage.Add(">>$CS_Please_Input_Software_Version");
				this.chbSoftName.Enabled = false;	
			}		
		}
		//Laws Lu,2005/12/28,比对版本
		private Messages CheckSoftVersion(object vc,string strVer,SoftwareActionEventArgs e,ProductInfo product)
		{	
			Messages msg = new Messages();
			if(e.CurrentMO == null)
			{
				e.CurrentMO = (new MOModel.MOFacade(DataProvider)).GetMO(product.LastSimulation.MOCode) as Domain.MOModel.MO;
			}

			if(e.CurrentMO.MOBIOSVersion != strVer)
			{
				msg.Add(new UserControl.Message(MessageType.Error,"$CS_VERSION_COMPARE_NOT_PASS $CS_MO_VERSION="
					+ e.CurrentMO.MOBIOSVersion + " $CS_CURRENT_VERSION=" + strVer));

				DataCollectFacade dcf = new DataCollectFacade(DataProvider);
				Domain.DataCollect.VersionError ve = dcf.CreateNewVersionError();
				ve.PKID = System.Guid.NewGuid().ToString();
				ve.MoVersionInfo = e.CurrentMO.MOBIOSVersion;
				ve.Mocode = e.CurrentMO.MOCode;
				ve.Rcard = e.RunningCard;
				ve.VersionInfo = strVer;
				//Laws Lu,2006/11/13 uniform system collect date
				DBDateTime dbDateTime;
				
				dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
				

				ve.MDate = dbDateTime.DBDate;
				ve.MTime = dbDateTime.DBTime;
				ve.MUser = e.UserCode;
				
				DataProvider.BeginTransaction();
				try
				{
					dcf.AddVersionError(ve);
					dcf.DeleteVersionCollect(vc as Domain.DataCollect.VersionCollect);

					DataProvider.CommitTransaction();
				}
				catch(Exception E)
				{
					msg.Add(new UserControl.Message(E));
					DataProvider.RollbackTransaction();
				}
				finally
				{
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				}
			}
			return msg;
		}

		//Laws Lu,2006/01/20,比对配置信息
		private Messages CheckConfig(SoftwareActionEventArgs e,ProductInfo product)
		{	
			Messages msg = new Messages();
			if(chkConfig.Checked == false)
			{
				return msg;
			}
			
			if(e.CurrentMO == null)
			{
				e.CurrentMO = (new MOModel.MOFacade(DataProvider)).GetMO(product.LastSimulation.MOCode) as Domain.MOModel.MO;
			}

			//			if(e.CurrentMO.MOBIOSVersion != strVer)
			//			{
			//				msg.Add(new UserControl.Message(MessageType.Error,"$CS_VERSION_COMPARE_NOT_PASS $CS_MO_VERSION="
			//					+ e.CurrentMO.MOBIOSVersion + " $CS_CURRENT_VERSION=" + strVer));

			DataCollectFacade dcf = new DataCollectFacade(DataProvider);
			MOModel.ItemFacade icf = new BenQGuru.eMES.MOModel.ItemFacade(DataProvider);

			object[] configInfos = dcf.GetConfigInfo(e.RunningCard);
			object[] itemChecks = icf.GetItem2Config(e.CurrentMO.ItemCode);

			//DataProvider.BeginTransaction();
			try
			{
				if(configInfos == null || itemChecks == null)
				{
					throw new Exception("$CS_CONFIG_NOT_EXIST");
				}
				int iPass = 0;
				foreach(Domain.DataCollect.ConfigInfo ci in configInfos)
				{
					dcf.DeleteConfigInfo(ci);
					foreach(Domain.MOModel.Item2Config ic in itemChecks)
					{
						if(ic.NeedCheck == "1" && ic.ConfigCode == ci.CheckItemCode
							&& ic.ParentCode == ci.CatergoryCode)
						{
							if(ic.ConfigValue != ci.CheckItemVlaue)
							{
								Domain.DataCollect.OnWipConfigCollect onwipConfigCollect = new BenQGuru.eMES.Domain.DataCollect.OnWipConfigCollect();

								onwipConfigCollect.PKID = System.Guid.NewGuid().ToString();
								onwipConfigCollect.RunningCard =  e.ProductInfo.NowSimulation.RunningCard;
								onwipConfigCollect.RunningCardSequence =  e.ProductInfo.NowSimulation.RunningCardSequence;
								onwipConfigCollect.ItemCode = e.CurrentMO.ItemCode;
								onwipConfigCollect.MoCode = e.CurrentMO.MOCode;

								onwipConfigCollect.ModelCode = e.ProductInfo.NowSimulation.ModelCode;
								onwipConfigCollect.SegmnetCode = e.ProductInfo.NowSimulationReport.SegmentCode;
								onwipConfigCollect.RouteCode = e.ProductInfo.NowSimulation.RouteCode;
								onwipConfigCollect.StepSequenceCode = e.ProductInfo.NowSimulationReport.StepSequenceCode;
								onwipConfigCollect.OPCode = e.ProductInfo.NowSimulation.OPCode;
								onwipConfigCollect.ResourceCode = e.ProductInfo.NowSimulation.ResourceCode;
								onwipConfigCollect.ShiftTypeCode = e.ProductInfo.NowSimulationReport.ShiftTypeCode;
								onwipConfigCollect.ShiftCode = e.ProductInfo.NowSimulationReport.ShiftCode;
								onwipConfigCollect.TimePeriodCode = e.ProductInfo.NowSimulationReport.TimePeriodCode;


								onwipConfigCollect.MaintainUser = e.ProductInfo.NowSimulation.MaintainUser;
								onwipConfigCollect.MaintainDate = e.ProductInfo.NowSimulation.MaintainDate;
								onwipConfigCollect.MaintainTime = e.ProductInfo.NowSimulation.MaintainTime;
								onwipConfigCollect.EAttribute1 = e.ProductInfo.NowSimulation.EAttribute1;

								onwipConfigCollect.ActValue = ci.CheckItemVlaue;
								onwipConfigCollect.CheckItemVlaue = ic.ConfigValue;
								onwipConfigCollect.ItemConfig = ic.ItemConfigration;
								onwipConfigCollect.CatergoryCode = ci.CatergoryCode;
								onwipConfigCollect.ParentCode = ic.ParentCode;
								onwipConfigCollect.CheckItemCode = ci.CheckItemCode;

								dcf.AddOnWipConfigCollect(onwipConfigCollect);

								msg.Add(new UserControl.Message(MessageType.Error,"$CS_CONFIG_CHECK_FAILURE " 
									+ ic.ConfigName + "=" + ic.ConfigValue + " $CS_Param_Action=" + ci.CheckItemVlaue));
							}
							else
							{
								iPass++;
							}
						}
					}
				}
				
				//DataProvider.CommitTransaction();
				if(msg.IsSuccess() && iPass != 0)
				{
					msg.Add(new UserControl.Message(MessageType.Success,"$CS_CONFIG_CHECK_SUCCESS $CS_CONFIG_CHECK_COUNT =" + iPass.ToString()));
				}
			}
			catch(Exception E)
			{
				msg.Add(new UserControl.Message(E));
				//DataProvider.RollbackTransaction();
			}
//			finally
//			{
//				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
//			}
			//Domain.MOModel.Item2Config[] configInfos = mfc.getitem(e.RunningCard);
			//Domain.MOModel.Item2Config[] itemConfigs = dcf.ge
			//			ve.PKID = System.Guid.NewGuid().ToString();
			//			ve.MoVersionInfo = e.CurrentMO.MOBIOSVersion;
			//			ve.Mocode = e.CurrentMO.MOCode;
			//			ve.Rcard = e.RunningCard;
			//			ve.VersionInfo = strVer;
			//			ve.MDate = FormatHelper.TODateInt(DateTime.Now);
			//			ve.MTime = FormatHelper.TOTimeInt(DateTime.Now);
			//			ve.MUser = e.UserCode;
			//				
			//			DataProvider.BeginTransaction();
			//			try
			//			{
			//				dcf.AddVersionError(ve);
			//				dcf.DeleteVersionCollect(vc as Domain.DataCollect.VersionCollect);
			//
			//				DataProvider.CommitTransaction();
			//			}
			//			catch(Exception E)
			//			{
			//				msg.Add(new UserControl.Message(E));
			//				DataProvider.RollbackTransaction();
			//			}
			//			finally
			//			{
			//				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
			//			}
			//			}
			return msg;
		}

		//执行Action
		private void doAction(object vc)
		{	
			Messages messages = this._helper.GetIDInfo(Convert.ToString(this.inputBuffer[0]));

			if ( messages.IsSuccess() )
			{
				ProductInfo product= (ProductInfo)messages.GetData().Values[0];

				if (product == null || 
					(product != null && product.LastSimulation == null))
				{
					messages.Add(new UserControl.Message(MessageType.Error,"$NoSimulation"));
				}
				//初始化参数
				SoftwareActionEventArgs args = new SoftwareActionEventArgs(
					ActionType.DataCollectAction_SoftINFO, 
					this.inputBuffer[0].ToString(), 
					ApplicationService.Current().UserCode,
					ApplicationService.Current().ResourceCode,
					(vc == null ? this.inputBuffer[2].ToString() : (vc as Domain.DataCollect.VersionCollect).VersionInfo)/*
					this.inputBuffer[2].ToString()*/,
					this.inputBuffer[1].ToString() );

				if(messages.IsSuccess())
				{
					if(args.CurrentMO == null)
					{
						args.CurrentMO = (new MOModel.MOFacade(DataProvider)).GetMO(product.LastSimulation.MOCode) as Domain.MOModel.MO;
					}
					if(chkAutoGetVersion.Checked == true && vc != null)
					{	//是否需要比对工单版本
						
						if(args.CurrentMO.IsCompareSoft == 1)
						{
							Messages msgs = CheckSoftVersion(vc,(vc as Domain.DataCollect.VersionCollect).VersionInfo.Trim().ToUpper(),args,product);
							if(msgs.IsSuccess())
							{
								this.ucMessage.Add(new UserControl.Message(MessageType.Success,"$CS_SOFT_CHECK_SUCCESS"));
							
							}
							else
							{
								this.ucMessage.Add(msgs);
							}
						}
					}
					if(chkAutoGetVersion.Checked == false)
					{	//是否需要比对工单版本
						
						if(args.CurrentMO.IsCompareSoft == 1)
						{
							if(this.inputBuffer[2].ToString().Trim().ToUpper() == args.CurrentMO.MOBIOSVersion)
							{
								this.ucMessage.Add(new UserControl.Message(MessageType.Success,"$CS_SOFT_CHECK_SUCCESS"));
							}
							else
							{
								this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_VERSION_COMPARE_NOT_PASS $CS_MO_VERSION="
									+ args.CurrentMO.MOBIOSVersion + " $CS_CURRENT_VERSION=" + this.inputBuffer[2].ToString().Trim().ToUpper()));


								DataCollectFacade dcf = new DataCollectFacade(DataProvider);
								Domain.DataCollect.VersionError ve = dcf.CreateNewVersionError();
								ve.PKID = System.Guid.NewGuid().ToString();
								ve.MoVersionInfo = args.CurrentMO.MOBIOSVersion;
								ve.Mocode = args.CurrentMO.MOCode;
								ve.Rcard = args.RunningCard;
								ve.VersionInfo = this.inputBuffer[2].ToString().Trim().ToUpper();

								//Laws Lu,2006/11/13 uniform system collect date
								DBDateTime dbDateTime;
							
								dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
							

								ve.MDate = dbDateTime.DBDate;
								ve.MTime = dbDateTime.DBTime;

								ve.MUser = args.UserCode;
				
								DataProvider.BeginTransaction();
								try
								{
									dcf.AddVersionError(ve);
									//dcf.DeleteVersionCollect(vc as Domain.DataCollect.VersionCollect);

									DataProvider.CommitTransaction();
								}
								catch(Exception E)
								{
									this.ucMessage.Add(new UserControl.Message(E));
									DataProvider.RollbackTransaction();
								}
								finally
								{
									((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
								}
							}
						}
					}
				
					IAction action = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_SoftINFO);

					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
					this.DataProvider.BeginTransaction();
					try
					{
						messages.AddMessages(action.Execute(args));	

						this.ucMessage.Add(CheckConfig(args,product));

						if ( messages.IsSuccess() )
						{
							this.DataProvider.CommitTransaction();	
//							if(chkConfig.Checked == true)
//							{
//								this.ucMessage.Add(new UserControl.Message(MessageType.Success,"$CS_CONFIG_CHECK_SUCCESS"));	//配置比对成功
//							}
							this.ucMessage.Add(new UserControl.Message(MessageType.Success,"$CS_Soft_CollectSuccess"));	//软件版本采集成功
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
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
					}
				}
			}
				
			this.ucMessage.Add( messages );
			ucMessage.Add(">>$CS_Please_Input_RunningCard ");
			ucLEInput.Value = String.Empty;
			this.inputBuffer.Clear();
			ucLEInput.TextFocus(false, true);
		}
	}
}
