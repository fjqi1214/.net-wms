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
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.AlertModel;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FFirstOffline 的摘要说明。
	/// </summary>
	public class FFirstOffline : System.Windows.Forms.Form
	{
		private UserControl.UCButton ucBtnOK;
		private UserControl.UCMessage ucMessage;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		private string _moCode = string.Empty;
		private System.Windows.Forms.Label lblOnlineInfo;
		private UserControl.UCLabelEdit ucRCard;
		private UserControl.UCLabelEdit ucSSName;
		private BenQGuru.eMES.AlertModel.FirstOnlineFacade _facade = null;
		private UserControl.UCMessage ucMessageOff;
		private System.Windows.Forms.Label lblOfflineInfo;
		private UserControl.UCLabelCombox cbxBeginTime;

		private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		public FFirstOffline()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			UserControl.UIStyleBuilder.FormUI(this);

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FFirstOffline));
			this.ucBtnOK = new UserControl.UCButton();
			this.ucMessage = new UserControl.UCMessage();
			this.lblOnlineInfo = new System.Windows.Forms.Label();
			this.ucRCard = new UserControl.UCLabelEdit();
			this.ucSSName = new UserControl.UCLabelEdit();
			this.ucMessageOff = new UserControl.UCMessage();
			this.lblOfflineInfo = new System.Windows.Forms.Label();
			this.cbxBeginTime = new UserControl.UCLabelCombox();
			this.SuspendLayout();
			// 
			// ucBtnOK
			// 
			this.ucBtnOK.BackColor = System.Drawing.SystemColors.Control;
			this.ucBtnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnOK.BackgroundImage")));
			this.ucBtnOK.ButtonType = UserControl.ButtonTypes.None;
			this.ucBtnOK.Caption = "确认下线";
			this.ucBtnOK.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucBtnOK.Location = new System.Drawing.Point(224, 40);
			this.ucBtnOK.Name = "ucBtnOK";
			this.ucBtnOK.Size = new System.Drawing.Size(88, 22);
			this.ucBtnOK.TabIndex = 1;
			this.ucBtnOK.Click += new System.EventHandler(this.ucBtnOK_Click);
			// 
			// ucMessage
			// 
			this.ucMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.ucMessage.BackColor = System.Drawing.Color.Gainsboro;
			this.ucMessage.ButtonVisible = false;
			this.ucMessage.Enabled = false;
			this.ucMessage.Location = new System.Drawing.Point(64, 72);
			this.ucMessage.Name = "ucMessage";
			this.ucMessage.Size = new System.Drawing.Size(682, 168);
			this.ucMessage.TabIndex = 0;
			// 
			// lblOnlineInfo
			// 
			this.lblOnlineInfo.Location = new System.Drawing.Point(8, 88);
			this.lblOnlineInfo.Name = "lblOnlineInfo";
			this.lblOnlineInfo.Size = new System.Drawing.Size(56, 23);
			this.lblOnlineInfo.TabIndex = 9;
			this.lblOnlineInfo.Text = "上线信息";
			// 
			// ucRCard
			// 
			this.ucRCard.AllowEditOnlyChecked = true;
			this.ucRCard.Caption = "首件标签";
			this.ucRCard.Checked = false;
			this.ucRCard.EditType = UserControl.EditTypes.String;
			this.ucRCard.Location = new System.Drawing.Point(8, 8);
			this.ucRCard.MaxLength = 400;
			this.ucRCard.Multiline = false;
			this.ucRCard.Name = "ucRCard";
			this.ucRCard.PasswordChar = '\0';
			this.ucRCard.ReadOnly = false;
			this.ucRCard.ShowCheckBox = false;
			this.ucRCard.Size = new System.Drawing.Size(262, 22);
			this.ucRCard.TabIndex = 0;
			this.ucRCard.TabNext = false;
			this.ucRCard.Value = "";
			this.ucRCard.WidthType = UserControl.WidthTypes.Long;
			this.ucRCard.XAlign = 70;
			this.ucRCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucRCard_TxtboxKeyPress);
			// 
			// ucSSName
			// 
			this.ucSSName.AllowEditOnlyChecked = true;
			this.ucSSName.Caption = "产线名";
			this.ucSSName.Checked = false;
			this.ucSSName.EditType = UserControl.EditTypes.String;
			this.ucSSName.Enabled = false;
			this.ucSSName.Location = new System.Drawing.Point(280, 8);
			this.ucSSName.MaxLength = 40;
			this.ucSSName.Multiline = false;
			this.ucSSName.Name = "ucSSName";
			this.ucSSName.PasswordChar = '\0';
			this.ucSSName.ReadOnly = false;
			this.ucSSName.ShowCheckBox = false;
			this.ucSSName.Size = new System.Drawing.Size(250, 22);
			this.ucSSName.TabIndex = 11;
			this.ucSSName.TabNext = false;
			this.ucSSName.Value = "";
			this.ucSSName.WidthType = UserControl.WidthTypes.Long;
			this.ucSSName.XAlign = 330;
			// 
			// ucMessageOff
			// 
			this.ucMessageOff.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.ucMessageOff.BackColor = System.Drawing.Color.Gainsboro;
			this.ucMessageOff.ButtonVisible = false;
			this.ucMessageOff.Enabled = false;
			this.ucMessageOff.Location = new System.Drawing.Point(64, 256);
			this.ucMessageOff.Name = "ucMessageOff";
			this.ucMessageOff.Size = new System.Drawing.Size(682, 168);
			this.ucMessageOff.TabIndex = 12;
			// 
			// lblOfflineInfo
			// 
			this.lblOfflineInfo.Location = new System.Drawing.Point(0, 256);
			this.lblOfflineInfo.Name = "lblOfflineInfo";
			this.lblOfflineInfo.Size = new System.Drawing.Size(56, 23);
			this.lblOfflineInfo.TabIndex = 13;
			this.lblOfflineInfo.Text = "下线信息";
			// 
			// cbxBeginTime
			// 
			this.cbxBeginTime.AllowEditOnlyChecked = true;
			this.cbxBeginTime.Caption = "上班时间";
			this.cbxBeginTime.Checked = false;
			this.cbxBeginTime.Location = new System.Drawing.Point(8, 40);
			this.cbxBeginTime.Name = "cbxBeginTime";
			this.cbxBeginTime.SelectedIndex = -1;
			this.cbxBeginTime.ShowCheckBox = false;
			this.cbxBeginTime.Size = new System.Drawing.Size(195, 21);
			this.cbxBeginTime.TabIndex = 18;
			this.cbxBeginTime.WidthType = UserControl.WidthTypes.Normal;
			this.cbxBeginTime.XAlign = 70;
			// 
			// FFirstOffline
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(752, 445);
			this.Controls.Add(this.cbxBeginTime);
			this.Controls.Add(this.lblOfflineInfo);
			this.Controls.Add(this.ucMessageOff);
			this.Controls.Add(this.ucSSName);
			this.Controls.Add(this.ucRCard);
			this.Controls.Add(this.lblOnlineInfo);
			this.Controls.Add(this.ucMessage);
			this.Controls.Add(this.ucBtnOK);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "FFirstOffline";
			this.Text = "首件下线";
			this.Load += new System.EventHandler(this.FFirstOffline_Load);
			this.Closed += new System.EventHandler(this.FFirstOffline_Closed);
			this.ResumeLayout(false);

		}
		#endregion

		private string _firstMsg;
		private string _offMsg;

		private void FFirstOffline_Closed(object sender, System.EventArgs e)
		{
			CloseConnection();  
		}

		private void CloseConnection()
		{
			if (this.DataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection(); 
		}

		private void FFirstOffline_Load(object sender, System.EventArgs e)
		{
			_firstMsg = UserControl.MutiLanguages.ParserMessage("$Error_FirstOn_Time");//{0}{1}产线首件上线时间是:{2};
			_offMsg = UserControl.MutiLanguages.ParserMessage("$Error_FirstOff_Time");//{0}{1}产线首件下线线时间是:{2},总共时间为{3}小时{4}分种

			this.ucRCard.TextFocus(false, true);
			Messages msg = new Messages();

			_facade = new FirstOnlineFacade(this.DataProvider);
			//取产线
			BenQGuru.eMES.BaseSetting.BaseModelFacade _baseFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
			Resource res = (Resource)_baseFacade.GetResource(ApplicationService.Current().ResourceCode);
			if(res != null && res.StepSequenceCode != null)
			{
				this.ucSSName.Value = res.StepSequenceCode;
			}
			else
			{
				msg.Add(new UserControl.Message(MessageType.Error,this.ucSSName.Caption + "$Error_Input_Empty")); 
				ApplicationRun.GetInfoForm().Add(msg);
				return;
			}
			this.ucRCard.InnerTextBox.Multiline = false;
		}

		
		private void DoAction()
		{
			try
			{
				this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
				Messages msg = new Messages();

				UIFirstOnline ufo = this.cbxBeginTime.ComboBoxData.SelectedItem as UIFirstOnline;
				if(ufo == null)
				{
					msg.Add(new UserControl.Message(MessageType.Error,"请选择上班时间")); 
					ApplicationRun.GetInfoForm().Add(msg);
					return;
				}

				//取得班次信息
				BenQGuru.eMES.BaseSetting.ShiftModelFacade bf = new BenQGuru.eMES.BaseSetting.ShiftModelFacade(this.DataProvider);
				BenQGuru.eMES.Domain.BaseSetting.Shift shift= bf.GetShift(ufo._first.ShiftCode) as BenQGuru.eMES.Domain.BaseSetting.Shift;
				if(shift == null)
				{
					msg.Add(new UserControl.Message(MessageType.Error,"班次不存在")); 
					ApplicationRun.GetInfoForm().Add(msg);
					return;
				}
	
				string itemcode = ufo._first.ItemCode;

				//2006/11/17,Laws Lu add get DateTime from db Server
				DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

				DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate,dbDateTime.DBTime);

				//DateTime dtNow = DateTime.Now;
				_facade = new FirstOnlineFacade(this.DataProvider);

				DateTime dtShiftDay = dtNow;
				
				/////如是跨天的班次，并且是处在第二天,则用前一天做为上线日期
				if(shift.IsOverDate == FormatHelper.TRUE_STRING)
				{
					if(FormatHelper.TOTimeInt(dtShiftDay) < shift.ShiftEndTime)
						dtShiftDay = dtShiftDay.AddDays(-1);
				}

				//object obj_on = _facade.GetFirstOnline(this.ucSSName.Value,FormatHelper.TODateInt(dtShiftDay),itemcode,ul._first.ShiftCode,FormatHelper.TOTimeInt(this.dtpBegin.Text));

				BenQGuru.eMES.Domain.Alert.FirstOnline first_on = ufo._first; //= obj_on as BenQGuru.eMES.Domain.Alert.FirstOnline;
//				if(first_on == null)
//				{
//					msg.Add(new UserControl.Message(MessageType.Error,"今天这个上班时间，此产线上此产品还没有首件上线"));//,"$Error_FirstOn_None")); //今天这个班次，此产线上此产品还没有首件上线
//					ApplicationRun.GetInfoForm().Add(msg);
//					return;
//				}
	
				if(first_on.ActionType == LineActionType.OFF)
				{
					msg.Add(new UserControl.Message(MessageType.Error,"今天这个上班时间，此产线上此产品已经有一个下线首件"));//"$Error_FirstOff_Repeat")); //今天这个班次，此产线上此产品已经有一个下线首件
					ApplicationRun.GetInfoForm().Add(msg);
					return;
				}

				try
				{
					
					first_on.OfflineRuningCard = this.ucRCard.InnerTextBox.Text.Trim();
					first_on.ActionType = LineActionType.OFF;
					first_on.OffLineTime = FormatHelper.TOTimeInt(dtNow);

					this.DataProvider.BeginTransaction();

					this.DataProvider.Update(first_on);
					
					TimeSpan it = DateTime.Parse(FormatHelper.ToTimeString(first_on.OffLineTime)) - DateTime.Parse(FormatHelper.ToTimeString(first_on.OnLineTime));

					//如果跨天，并且是在第二天
					if(shift.IsOverDate == FormatHelper.TRUE_STRING && FormatHelper.TOTimeInt(dtShiftDay) < shift.ShiftEndTime)
					{
						it = DateTime.Parse(FormatHelper.ToTimeString(first_on.OffLineTime)).AddDays(1) - DateTime.Parse(FormatHelper.ToTimeString(first_on.OnLineTime));		
					}
					

					ucMessage.Add(new UserControl.Message("上班时间"+FormatHelper.ToTimeString(first_on.ShiftTime)+"," +
														String.Format(_firstMsg,
																		(DateTime.Parse((FormatHelper.ToDateString(first_on.MaintainDate)))).ToLongDateString(),
																		first_on.SSCode,
																		first_on.ItemCode,
																		FormatHelper.ToTimeString(first_on.OnLineTime)
																		)
															)
									);

					ucMessageOff.Add(new UserControl.Message("上班时间"+FormatHelper.ToTimeString(first_on.ShiftTime)+"," +
															String.Format(_offMsg,
																			(DateTime.Parse((FormatHelper.ToDateString(first_on.MaintainDate)))).ToLongDateString(),
																			first_on.SSCode,
																			first_on.ItemCode,
																			FormatHelper.ToTimeString(first_on.OffLineTime),
																			it.Hours,
																			it.Minutes																		
																			)
															)
									);

					this.DataProvider.CommitTransaction();
					msg.Add(new UserControl.Message(MessageType.Success,"$Error_FirstOff_Sucess")); //首件下线成功
					ApplicationRun.GetInfoForm().Add(msg);
					this.cbxBeginTime.ComboBoxData.Items.Clear();
				}
				catch(System.Exception ex)
				{
					this.DataProvider.RollbackTransaction();
					msg.Add(new UserControl.Message(MessageType.Error,ex.Message)); 
					ApplicationRun.GetInfoForm().Add(msg);
				}
			}
			finally
			{
				this.Cursor = System.Windows.Forms.Cursors.Arrow;
				this.ucRCard.Value = string.Empty;
				this.ucRCard.TextFocus(false, true);
				CloseConnection();
			}
		}
		

		private void ucRCard_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar=='\r')
			{
				FillBegin();	
			}
		}

		//根据产线，产品，Fill上班时间下拉列表
		private void FillBegin()
		{
			#region 检查界面输入
			if(this.ucSSName.Value == string.Empty)
			{
				Messages msg = new Messages();
				msg.Add(new UserControl.Message(MessageType.Error,this.ucSSName.Caption + " $Error_Input_Empty"));
				ApplicationRun.GetInfoForm().Add(msg);
				this.ucRCard.TextFocus(false, true);
				return;
			}
			if(this.ucRCard.Value == string.Empty)
			{
				Messages msg = new Messages();
				msg.Add(new UserControl.Message(MessageType.Error,this.ucRCard.Caption + " $Error_Input_Empty"));
				ApplicationRun.GetInfoForm().Add(msg);
				this.ucRCard.TextFocus(false, true);
				return;
			}
			#endregion
			
			#region 判断产品序列号是否存在
			BenQGuru.eMES.DataCollect.DataCollectFacade dcFacade = new DataCollectFacade(this.DataProvider);
			BenQGuru.eMES.Domain.DataCollect.Simulation sim = dcFacade.GetSimulation(ucRCard.InnerTextBox.Text.Trim()) as BenQGuru.eMES.Domain.DataCollect.Simulation;
			
			string itemcode;
			if(sim == null)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$Error_First_No_Rcard"));
				ucRCard.TextFocus(false, true);
				return;
			}
			else
			{
				itemcode = sim.ItemCode;
			}
			#endregion

			#region Build下班时间下拉列表
			this.cbxBeginTime.ComboBoxData.Items.Clear();
			object[] objs = this._facade.QueryFirst(this.ucSSName.Value,itemcode);
			if(objs != null && objs.Length > 0)
			{
				foreach(BenQGuru.eMES.Domain.Alert.FirstOnline last in objs)
				{
					if(last != null)
					{
						this.cbxBeginTime.ComboBoxData.Items.Add(new UIFirstOnline(last));
					}
				}
			}
			else
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"请先做上线"));
				ucRCard.TextFocus(false, true);
				return;
			}

			if(this.cbxBeginTime.ComboBoxData.Items.Count > 0)
				this.cbxBeginTime.ComboBoxData.SelectedIndex = 0;
			#endregion
		}

		private void ucBtnOK_Click(object sender, System.EventArgs e)
		{
			if(this.ucSSName.Value == string.Empty)
			{
				Messages msg = new Messages();
				msg.Add(new UserControl.Message(MessageType.Error,this.ucSSName.Caption + " $Error_Input_Empty"));
				ApplicationRun.GetInfoForm().Add(msg);
				this.ucRCard.TextFocus(false, true);
				return;
			}
			if(this.ucRCard.Value == string.Empty)
			{
				Messages msg = new Messages();
				msg.Add(new UserControl.Message(MessageType.Error,this.ucRCard.Caption + " $Error_Input_Empty"));
				ApplicationRun.GetInfoForm().Add(msg);
				this.ucRCard.TextFocus(false, true);
				return;
			}
			DoAction();	
			this.ucRCard.TextFocus(false, true);
		}
	}
}
