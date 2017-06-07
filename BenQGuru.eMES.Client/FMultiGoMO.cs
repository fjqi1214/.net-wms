using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

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
	/// FMultiGoMO 的摘要说明。
	/// </summary>
	public class FMultiGoMO : System.Windows.Forms.Form
	{

		private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;

		private UserControl.UCLabelEdit txtMoCode;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private UserControl.UCLabelEdit txtRCard;
		private UserControl.UCLabelEdit txtSumNum;
		private UserControl.UCButton ucBtnExit;
		private System.Windows.Forms.Panel panel3;
		private UserControl.UCMessage ucMessage;
		private BenQGuru.eMES.Client.Data.dsMo dsMo;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdMoList;
		private System.Windows.Forms.Panel panel4;
		private UserControl.UCButton bntLock;
		private System.Windows.Forms.CheckBox chkNeedMemo;
		private UserControl.UCLabelEdit bRCardLenULE;
		private UserControl.UCLabelEdit bRCardLetterULE;
	
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FMultiGoMO()
		{
			UserControl.UIStyleBuilder.FormUI(this);
			
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			grdMoList.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White;;
			grdMoList.DisplayLayout.CaptionAppearance.BackColor =Color.FromName("WhiteSmoke");
			grdMoList.DisplayLayout.Appearance.BackColor=Color.FromArgb(255, 255, 255);
			grdMoList.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
			grdMoList.DisplayLayout.Override.RowAppearance.BackColor =Color.FromArgb(230, 234, 245);
			grdMoList.DisplayLayout.Override.RowAlternateAppearance.BackColor=Color.FromArgb(255, 255, 255);
			grdMoList.DisplayLayout.Override.RowSelectors =Infragistics.Win.DefaultableBoolean.False;
			grdMoList.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
			grdMoList.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
			grdMoList.DisplayLayout.ScrollBarLook.Appearance.BackColor =Color.FromName("LightGray");

			ucMessage.Add(">>$CS_Please_Input_MOCode");

			txtMoCode.TextFocus(false, true);
			//UserControl.UIStyleBuilder.GridUI(grdMoList);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMultiGoMO));
            this.txtMoCode = new UserControl.UCLabelEdit();
            this.dsMo = new BenQGuru.eMES.Client.Data.dsMo();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bRCardLetterULE = new UserControl.UCLabelEdit();
            this.bRCardLenULE = new UserControl.UCLabelEdit();
            this.chkNeedMemo = new System.Windows.Forms.CheckBox();
            this.bntLock = new UserControl.UCButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucBtnExit = new UserControl.UCButton();
            this.txtSumNum = new UserControl.UCLabelEdit();
            this.txtRCard = new UserControl.UCLabelEdit();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grdMoList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ucMessage = new UserControl.UCMessage();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dsMo)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMoList)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMoCode
            // 
            this.txtMoCode.AllowEditOnlyChecked = true;
            this.txtMoCode.Caption = "工单";
            this.txtMoCode.Checked = false;
            this.txtMoCode.EditType = UserControl.EditTypes.String;
            this.txtMoCode.Location = new System.Drawing.Point(25, 16);
            this.txtMoCode.MaxLength = 80;
            this.txtMoCode.Multiline = false;
            this.txtMoCode.Name = "txtMoCode";
            this.txtMoCode.PasswordChar = '\0';
            this.txtMoCode.ReadOnly = false;
            this.txtMoCode.ShowCheckBox = false;
            this.txtMoCode.Size = new System.Drawing.Size(525, 24);
            this.txtMoCode.TabIndex = 1;
            this.txtMoCode.TabNext = false;
            this.txtMoCode.Value = "";
            this.txtMoCode.WidthType = UserControl.WidthTypes.TooLong;
            this.txtMoCode.XAlign = 69;
            this.txtMoCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMoCode_TxtboxKeyPress);
            // 
            // dsMo
            // 
            this.dsMo.DataSetName = "dsMo";
            this.dsMo.Locale = new System.Globalization.CultureInfo("en-US");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bRCardLetterULE);
            this.panel1.Controls.Add(this.bRCardLenULE);
            this.panel1.Controls.Add(this.chkNeedMemo);
            this.panel1.Controls.Add(this.bntLock);
            this.panel1.Controls.Add(this.txtMoCode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(752, 80);
            this.panel1.TabIndex = 9;
            // 
            // bRCardLetterULE
            // 
            this.bRCardLetterULE.AllowEditOnlyChecked = true;
            this.bRCardLetterULE.Caption = "产品序列号首字符检查";
            this.bRCardLetterULE.Checked = false;
            this.bRCardLetterULE.EditType = UserControl.EditTypes.String;
            this.bRCardLetterULE.Location = new System.Drawing.Point(346, 46);
            this.bRCardLetterULE.MaxLength = 40;
            this.bRCardLetterULE.Multiline = false;
            this.bRCardLetterULE.Name = "bRCardLetterULE";
            this.bRCardLetterULE.PasswordChar = '\0';
            this.bRCardLetterULE.ReadOnly = false;
            this.bRCardLetterULE.ShowCheckBox = true;
            this.bRCardLetterULE.Size = new System.Drawing.Size(339, 24);
            this.bRCardLetterULE.TabIndex = 24;
            this.bRCardLetterULE.TabNext = false;
            this.bRCardLetterULE.Value = "";
            this.bRCardLetterULE.WidthType = UserControl.WidthTypes.Normal;
            this.bRCardLetterULE.XAlign = 525;
            // 
            // bRCardLenULE
            // 
            this.bRCardLenULE.AllowEditOnlyChecked = true;
            this.bRCardLenULE.Caption = "产品序列号长度检查";
            this.bRCardLenULE.Checked = false;
            this.bRCardLenULE.EditType = UserControl.EditTypes.Integer;
            this.bRCardLenULE.Location = new System.Drawing.Point(16, 48);
            this.bRCardLenULE.MaxLength = 40;
            this.bRCardLenULE.Multiline = false;
            this.bRCardLenULE.Name = "bRCardLenULE";
            this.bRCardLenULE.PasswordChar = '\0';
            this.bRCardLenULE.ReadOnly = false;
            this.bRCardLenULE.ShowCheckBox = true;
            this.bRCardLenULE.Size = new System.Drawing.Size(324, 24);
            this.bRCardLenULE.TabIndex = 23;
            this.bRCardLenULE.TabNext = false;
            this.bRCardLenULE.Value = "";
            this.bRCardLenULE.WidthType = UserControl.WidthTypes.Normal;
            this.bRCardLenULE.XAlign = 180;
            // 
            // chkNeedMemo
            // 
            this.chkNeedMemo.Location = new System.Drawing.Point(648, 16);
            this.chkNeedMemo.Name = "chkNeedMemo";
            this.chkNeedMemo.Size = new System.Drawing.Size(104, 24);
            this.chkNeedMemo.TabIndex = 13;
            this.chkNeedMemo.Text = "备注";
            // 
            // bntLock
            // 
            this.bntLock.BackColor = System.Drawing.SystemColors.Control;
            this.bntLock.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bntLock.BackgroundImage")));
            this.bntLock.ButtonType = UserControl.ButtonTypes.None;
            this.bntLock.Caption = "锁定";
            this.bntLock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntLock.Location = new System.Drawing.Point(552, 16);
            this.bntLock.Name = "bntLock";
            this.bntLock.Size = new System.Drawing.Size(88, 22);
            this.bntLock.TabIndex = 12;
            this.bntLock.TabStop = false;
            this.bntLock.Click += new System.EventHandler(this.bntLock_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucBtnExit);
            this.panel2.Controls.Add(this.txtSumNum);
            this.panel2.Controls.Add(this.txtRCard);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 444);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(752, 49);
            this.panel2.TabIndex = 10;
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(597, 16);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 5;
            // 
            // txtSumNum
            // 
            this.txtSumNum.AllowEditOnlyChecked = true;
            this.txtSumNum.Caption = "已采集数量";
            this.txtSumNum.Checked = false;
            this.txtSumNum.EditType = UserControl.EditTypes.String;
            this.txtSumNum.Location = new System.Drawing.Point(335, 16);
            this.txtSumNum.MaxLength = 40;
            this.txtSumNum.Multiline = false;
            this.txtSumNum.Name = "txtSumNum";
            this.txtSumNum.PasswordChar = '\0';
            this.txtSumNum.ReadOnly = true;
            this.txtSumNum.ShowCheckBox = false;
            this.txtSumNum.Size = new System.Drawing.Size(247, 24);
            this.txtSumNum.TabIndex = 3;
            this.txtSumNum.TabNext = false;
            this.txtSumNum.Value = "0";
            this.txtSumNum.WidthType = UserControl.WidthTypes.Normal;
            this.txtSumNum.XAlign = 423;
            // 
            // txtRCard
            // 
            this.txtRCard.AllowEditOnlyChecked = true;
            this.txtRCard.Caption = "输入框";
            this.txtRCard.Checked = false;
            this.txtRCard.EditType = UserControl.EditTypes.String;
            this.txtRCard.Location = new System.Drawing.Point(31, 16);
            this.txtRCard.MaxLength = 40;
            this.txtRCard.Multiline = false;
            this.txtRCard.Name = "txtRCard";
            this.txtRCard.PasswordChar = '\0';
            this.txtRCard.ReadOnly = false;
            this.txtRCard.ShowCheckBox = false;
            this.txtRCard.Size = new System.Drawing.Size(299, 24);
            this.txtRCard.TabIndex = 2;
            this.txtRCard.TabNext = false;
            this.txtRCard.Value = "";
            this.txtRCard.WidthType = UserControl.WidthTypes.Long;
            this.txtRCard.XAlign = 90;
            this.txtRCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRCard_TxtboxKeyPress);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.grdMoList);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 80);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(752, 208);
            this.panel3.TabIndex = 11;
            // 
            // grdMoList
            // 
            this.grdMoList.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdMoList.DataSource = this.dsMo.MultiMo;
            this.grdMoList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMoList.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grdMoList.Location = new System.Drawing.Point(0, 0);
            this.grdMoList.Name = "grdMoList";
            this.grdMoList.Size = new System.Drawing.Size(752, 208);
            this.grdMoList.TabIndex = 12;
            // 
            // ucMessage
            // 
            this.ucMessage.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessage.ButtonVisible = false;
            this.ucMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessage.Location = new System.Drawing.Point(0, 0);
            this.ucMessage.Name = "ucMessage";
            this.ucMessage.Size = new System.Drawing.Size(752, 156);
            this.ucMessage.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ucMessage);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 288);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(752, 156);
            this.panel4.TabIndex = 12;
            // 
            // FMultiGoMO
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(752, 493);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "FMultiGoMO";
            this.Text = "多工单归属";
            this.Closed += new System.EventHandler(this.FMultiGoMO_Closed);
            ((System.ComponentModel.ISupportInitialize)(this.dsMo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMoList)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void FMultiGoMO_Closed(object sender, System.EventArgs e)
		{
			if (this.DataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();  
		}
		//输入项次
		private int CurrentSequence = 1;
		//缓存的工单
		private string[] MoCodes = null;

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
		//备注 
		public string Memo = String.Empty;

		private void txtMoCode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar =='\r')
			{
				if(txtMoCode.Value.Trim() != String.Empty)
				{
					Messages msg = new Messages();
					string[] MOs = ParseMo(Web.Helper.FormatHelper.CleanString(txtMoCode.Value.Trim().ToUpper()));
					msg.AddMessages(UpdateList(MOs));

					if(msg.IsSuccess())
					{
						LockMoInput();

						ucMessage.Add(">>$CS_Please_Input_RunningCard " + CurrentSequence.ToString() + "/" + dsMo.MultiMo.Rows.Count);//请输入产品序列号

						txtRCard.TextFocus(false, true);
					}
					else
					{
						ucMessage.Add(msg);

						txtMoCode.TextFocus(false, true);
					}
				}
				else
				{
					dsMo.MultiMo.Rows.Clear();

					ucMessage.Add(">>$CS_Please_Input_MOCode");

					txtMoCode.TextFocus(false, true);
				}
			}
		}

		#region 输入MO后，更新List显示
		//根据用户输入，获取工单s
		private string[] ParseMo(string input)
		{
			string[] MOs = input.Split(new char[]{','});
			MoCodes = new string[MOs.Length];

			Array.Copy(MOs,MoCodes,MOs.Length);

			return MOs;
		}
		//更新显示列表
		private Messages UpdateList(string[] moCodes)
		{
			Messages msg =  new Messages();
			CurrentSequence = 1;
			dsMo.Clear();

			MOModel.MOFacade moFAC =  new MOModel.MOFacade(this.DataProvider);
			foreach(string moCode in moCodes)
			{
				//获取MO
				object obj = moFAC.GetMO(moCode);
				
				if(obj != null)
				{
					Domain.MOModel.MO mo = obj as Domain.MOModel.MO;

					if(mo.MOStatus != Web.Helper.MOManufactureStatus.MOSTATUS_OPEN 
						&& mo.MOStatus != Web.Helper.MOManufactureStatus.MOSTATUS_RELEASE)
					{
						msg.Add(new UserControl.Message(MessageType.Error,"$CS_MO_Status_Error $CS_Param_MOStatus=$" 
							+ mo.MOStatus + " $Domain_MO=" + mo.MOCode));
						break;
					}
					//将MO添加到列表中
					int moRestQty = Convert.ToInt32(mo.MOPlanQty - mo.MOInputQty  + mo.MOScrapQty+ mo.MOOffQty);
					try
					{
						bool isExistMo = false;

						foreach(DataRow dr in dsMo.MultiMo.Rows)
						{
							if(Convert.ToString(dr["工单"]) == mo.MOCode)
							{
								isExistMo = true;
								break;
							}
						}
						if(!isExistMo)
						{
							dsMo.MultiMo.Rows.Add(new object[]{
																  mo.MOCode
																  ,""
																  ,mo.ItemCode
																  ,Convert.ToInt32(mo.MOInputQty)
																  ,Convert.ToInt32(mo.MOPlanQty)
																  ,Convert.ToInt32(mo.MOScrapQty)
																  ,Convert.ToInt32(mo.MOOffQty)
																  ,Convert.ToInt32(mo.MOActualQty)
																  ,moRestQty});
							dsMo.AcceptChanges();
						}
						else
						{/*需要修改*/
							msg.Add(new UserControl.Message(MessageType.Error,"$CS_MOCODE_REPEAT $Domain_MO=" + moCode));
						}
					}
					catch(Exception E)
					{
						msg.Add(new UserControl.Message(E));
					}
				}
				else
				{
					msg.Add(new UserControl.Message(MessageType.Error,"$CS_MO_Not_Exist $Domain_MO=" + moCode));
					break;
				}
			}
			return msg;
		}
		#endregion

		private Messages MatchMo(string rcard,int rowNumber)
		{
			Messages msg = new Messages();
			string moCode = String.Empty;
			
			if(dsMo.MultiMo.Rows.Count < 1)
			{
				msg.Add(new UserControl.Message(MessageType.Error, "$CS_CMPleaseInputMO"));
				txtMoCode.TextFocus(false, true);
			}
			else
			{
				moCode = Convert.ToString(dsMo.MultiMo.Rows[rowNumber]["工单"]);
			}

//			Messages msg = new Messages();
//			// 检查序列号格式
//			if ( rcard.Length < 7 )
//			{
//				msg.Add(new UserControl.Message(MessageType.Error,"$CS_RunningCard_Format_Error $CS_Param_ID=" + rcard));
//			}
//			// 产品序列号的第二码与工单的第一位相同
//			else if ( rcard[1] != moCode[0] )
//			{
//				msg.Add(new UserControl.Message(MessageType.Error,"$CS_RunningCard_Format_Error $CS_Param_ID=" + rcard));
//			}
//			// 产品序列号的3－7码与工单的最后5位相同
//			else if ( rcard.Substring(2, 5) != moCode.Substring(moCode.Length-5 < 0 ? 0 : moCode.Length - 5 ) )
//			{
//				msg.Add(new UserControl.Message(MessageType.Error,"$CS_RunningCard_Format_Error $CS_Param_ID=" + rcard));
//			}

			return msg;
		}
		//根据输入的Rcard，获取工单列表中匹配的行号
		private int GetMathRow(string rcard)
		{
			int iReturn = -1;
			for(int iRow = 0;iRow < dsMo.MultiMo.Rows.Count;iRow ++)
			{
				if(clsRCard2Mo.MatchMo(rcard,Convert.ToString(dsMo.MultiMo.Rows[iRow]["工单"])))
				{
					iReturn = iRow;
					break;
				}
			}
			return iReturn;
		}

		#region RCard List操作
		private void UpdateListRCard(int rowNumber)
		{
			if(Convert.ToString(dsMo.MultiMo.Rows[rowNumber]["产品序列号"]) == String.Empty)
			{
				dsMo.MultiMo.Rows[rowNumber]["产品序列号"] = Web.Helper.FormatHelper.CleanString(txtRCard.Value.Trim().ToUpper());
				dsMo.MultiMo.AcceptChanges();
			}
			else
			{
				dsMo.MultiMo.Rows[rowNumber]["产品序列号"] = Web.Helper.FormatHelper.CleanString(txtRCard.Value.Trim().ToUpper());
				dsMo.MultiMo.AcceptChanges();

				CurrentSequence = CurrentSequence -1;
			}
		}

		private void ClearListRCard()
		{
			foreach(DataRow dr in dsMo.MultiMo)
			{
				dr["产品序列号"] = String.Empty;
			}
			dsMo.MultiMo.Rows.Clear();

			dsMo.AcceptChanges();
		}

		private void UpdateCollectQty()
		{
			txtSumNum.Value = Convert.ToString(Convert.ToInt32(FormatHelper.CleanString(txtSumNum.Value.Trim())) 
				+ (MoCodes == null ?0 : MoCodes.Length));
		}
		#endregion

		private void txtRCard_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar =='\r')
			{
				UserControl.Messages msg = new UserControl.Messages();
				
				string rcard = Web.Helper.FormatHelper.CleanString(txtRCard.Value.Trim().ToUpper());
				//检查产品序列号格式
				msg.AddMessages(MatchMo(rcard,CurrentSequence - 1));
				if(!msg.IsSuccess())
				{
					ucMessage.Add(msg);
                    this.txtMoCode.TextFocus(false, true);
                    return;
				}
				else
				{
					#region 业务处理
					//如果为最后一次，则提交更新
					if(CurrentSequence == dsMo.MultiMo.Rows.Count)
					{
						//更新RunningCard
						UpdateListRCard(CurrentSequence - 1);

						ActionOnLineHelper _helper = new ActionOnLineHelper(this.DataProvider);

						DataProvider.BeginTransaction();
						try
						{
							#region 循环完成归属工单
							foreach(DataRow dr in dsMo.MultiMo)
							{
								string runningCard = Convert.ToString(dr["产品序列号"]);
								string moCode = Convert.ToString(dr["工单"]);

								msg.AddMessages(_helper.GetIDInfo(runningCard));

								if (msg.IsSuccess() )
								{
									ProductInfo product= (ProductInfo)msg.GetData().Values[0];

									GoToMOActionEventArgs args = new GoToMOActionEventArgs( 
										ActionType.DataCollectAction_GoMO, 
										runningCard, 
										ApplicationService.Current().UserCode,
										ApplicationService.Current().ResourceCode,
										product, 
										moCode);

									IAction action = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_GoMO);

									msg.AddMessages(action.Execute(args));	
								}
							
						
								if ( msg.IsSuccess() )
								{
									this.DataProvider.CommitTransaction();

									UpdateCollectQty();

									msg.Add( new UserControl.Message(MessageType.Success,"$CS_GOMO_CollectSuccess $CS_GONEXT_BILL"));
								}
								else
								{
									ClearListRCard();
									this.DataProvider.RollbackTransaction();
									break;
								}
							}
							#endregion
						}
						catch(Exception E)
						{
							ClearListRCard();
							this.DataProvider.RollbackTransaction();

							msg.Add(new UserControl.Message(E));
						}
						finally
						{
							//Laws Lu,2005/10/19,新增	缓解性能问题
							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();

							ucMessage.Add( msg );
					
							if(MoCodes != null && MoCodes.Length > 0)
							{
								UpdateList(MoCodes);
							}
							//CurrentSequence = 1;
							//dsMo.MultiMo.AcceptChanges();
						
						}
					}
					else
					{
						//更新RunningCard
						UpdateListRCard(CurrentSequence - 1);

						CurrentSequence ++;
					
					}
					#endregion
				}
				//提示用户继续输入产品序列号

				ucMessage.Add(">>$CS_Please_Input_RunningCard " + CurrentSequence.ToString() + "/" + dsMo.MultiMo.Rows.Count.ToString());
				txtRCard.Value = "";
				txtRCard.TextFocus(false, true);
			}
		}

		private Messages DoAction(string rcard,int iRow)
		{
			Messages msg = new Messages();
			if((chkNeedMemo.Checked == true && CurrentSequence <= dsMo.MultiMo.Rows.Count)
				|| chkNeedMemo.Checked == false)
			{
				msg.AddMessages(MatchMo(rcard,iRow));
			}

			if(!msg.IsSuccess())
			{
				ucMessage.Add(msg);
			}
			else
			{
				
				if(chkNeedMemo.Checked == true && CurrentSequence > dsMo.MultiMo.Rows.Count)
				{
					Memo = rcard;
				}
				else
				{
					//更新RunningCard
					UpdateListRCard(iRow);
				}

				#region 业务处理
				//如果为最后一次，则提交更新
				if((CurrentSequence == dsMo.MultiMo.Rows.Count && chkNeedMemo.Checked == false)
					|| ((CurrentSequence > dsMo.MultiMo.Rows.Count && chkNeedMemo.Checked == true && Memo != String.Empty)))
				{
					ActionOnLineHelper _helper = new ActionOnLineHelper(this.DataProvider);

					DataProvider.BeginTransaction();
					try
					{
						#region 循环完成归属工单
						foreach(DataRow dr in dsMo.MultiMo)
						{
							string runningCard = Convert.ToString(dr["产品序列号"]);
							string moCode = Convert.ToString(dr["工单"]);

							msg.AddMessages(_helper.GetIDInfo(runningCard));

							if (msg.IsSuccess() )
							{
								ProductInfo product= (ProductInfo)msg.GetData().Values[0];

								GoToMOActionEventArgs args = new GoToMOActionEventArgs( 
									ActionType.DataCollectAction_GoMO, 
									runningCard, 
									ApplicationService.Current().UserCode,
									ApplicationService.Current().ResourceCode,
									product, 
									moCode);

							
								args.Memo = Memo;
								IAction action = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_GoMO);

								msg.AddMessages(action.Execute(args));	
							}
						}
						
						if ( msg.IsSuccess() )
						{
							this.DataProvider.CommitTransaction();

							UpdateCollectQty();

							Memo = String.Empty;

							msg.Add( new UserControl.Message(MessageType.Success,"$CS_GOMO_CollectSuccess $CS_GONEXT_BILL"));
						}
						else
						{
							ClearListRCard();
							this.DataProvider.RollbackTransaction();
						}
						#endregion
					}
					catch(Exception E)
					{
						ClearListRCard();
						this.DataProvider.RollbackTransaction();

						msg.Add(new UserControl.Message(E));
					}
					finally
					{
						//Laws Lu,2005/10/19,新增	缓解性能问题
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();

						//ucMessage.Add( msg );
					
						if(MoCodes != null && MoCodes.Length > 0)
						{
							UpdateList(MoCodes);
						}
						//CurrentSequence = 1;
						//dsMo.MultiMo.AcceptChanges();
						
					}
						
				}
				else
				{
					
					CurrentSequence ++;
					
				}
				#endregion

			}

			return msg;
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
	}
}
