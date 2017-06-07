using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.MOModel;
using UserControl;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FCollectionIDMerge 的摘要说明。
	/// </summary>
	public class FCollectionIDMerge : BaseForm
    {
        #region Controls
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
		private UserControl.UCMessage ucMessage;
		private UserControl.UCButton ucBtnCancel;
		private UserControl.UCButton ucBtnExit;
		private UserControl.UCButton ucBtnOK;
		private UserControl.UCButton ucBtnRecede;
		public UserControl.UCLabelEdit ucLERunningCard;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
        private System.ComponentModel.Container components = null;
		private int _currSequence = 0;
		private int _mergeRule = 0;
		private ArrayList _runningCardList = null;
		private ProductInfo _product = null;
		private ActionOnLineHelper _helper = null;
		private string _idMergeType = string.Empty;
		private System.Windows.Forms.TextBox CollectedCount;
		private System.Windows.Forms.Label lblConvertQty;
        private System.Windows.Forms.Splitter splitter1;
		private UserControl.UCLabelEdit checkMO;
		private System.Windows.Forms.CheckBox chkUndo;

        private UCLabelEdit bCardTransLenULE;
        private UCLabelEdit aCardTransLenULE;
        private UCLabelEdit aCardTransLetterULE;
        private UCLabelEdit bCardTransLetterULE;

        #endregion

        private string _tCard = string.Empty;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;

		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		private object[] transedRunningCardByProduct = null;	// Added by Icyer 2006/11/08
		public FCollectionIDMerge()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCollectionIDMerge));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkUndo = new System.Windows.Forms.CheckBox();
            this.CollectedCount = new System.Windows.Forms.TextBox();
            this.lblConvertQty = new System.Windows.Forms.Label();
            this.ucBtnRecede = new UserControl.UCButton();
            this.ucBtnCancel = new UserControl.UCButton();
            this.ucLERunningCard = new UserControl.UCLabelEdit();
            this.ucBtnExit = new UserControl.UCButton();
            this.ucBtnOK = new UserControl.UCButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.aCardTransLetterULE = new UserControl.UCLabelEdit();
            this.aCardTransLenULE = new UserControl.UCLabelEdit();
            this.bCardTransLenULE = new UserControl.UCLabelEdit();
            this.checkMO = new UserControl.UCLabelEdit();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.bCardTransLetterULE = new UserControl.UCLabelEdit();
            this.ucMessage = new UserControl.UCMessage();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkUndo);
            this.groupBox1.Controls.Add(this.CollectedCount);
            this.groupBox1.Controls.Add(this.lblConvertQty);
            this.groupBox1.Controls.Add(this.ucBtnRecede);
            this.groupBox1.Controls.Add(this.ucBtnCancel);
            this.groupBox1.Controls.Add(this.ucLERunningCard);
            this.groupBox1.Controls.Add(this.ucBtnExit);
            this.groupBox1.Controls.Add(this.ucBtnOK);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 496);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(785, 90);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chkUndo
            // 
            this.chkUndo.Location = new System.Drawing.Point(277, 19);
            this.chkUndo.Name = "chkUndo";
            this.chkUndo.Size = new System.Drawing.Size(201, 24);
            this.chkUndo.TabIndex = 15;
            this.chkUndo.Text = "更改转换结果";
            // 
            // CollectedCount
            // 
            this.CollectedCount.Location = new System.Drawing.Point(553, 21);
            this.CollectedCount.Name = "CollectedCount";
            this.CollectedCount.ReadOnly = true;
            this.CollectedCount.Size = new System.Drawing.Size(61, 21);
            this.CollectedCount.TabIndex = 14;
            this.CollectedCount.Text = "0";
            // 
            // lblConvertQty
            // 
            this.lblConvertQty.Location = new System.Drawing.Point(477, 24);
            this.lblConvertQty.Name = "lblConvertQty";
            this.lblConvertQty.Size = new System.Drawing.Size(72, 15);
            this.lblConvertQty.TabIndex = 13;
            this.lblConvertQty.Text = "转换数量";
            // 
            // ucBtnRecede
            // 
            this.ucBtnRecede.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnRecede.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnRecede.BackgroundImage")));
            this.ucBtnRecede.ButtonType = UserControl.ButtonTypes.Change;
            this.ucBtnRecede.Caption = "更正";
            this.ucBtnRecede.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnRecede.Location = new System.Drawing.Point(307, 52);
            this.ucBtnRecede.Name = "ucBtnRecede";
            this.ucBtnRecede.Size = new System.Drawing.Size(88, 22);
            this.ucBtnRecede.TabIndex = 3;
            this.ucBtnRecede.Click += new System.EventHandler(this.ucBtnRecede_Click);
            // 
            // ucBtnCancel
            // 
            this.ucBtnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnCancel.BackgroundImage")));
            this.ucBtnCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.ucBtnCancel.Caption = "取消";
            this.ucBtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnCancel.Location = new System.Drawing.Point(200, 52);
            this.ucBtnCancel.Name = "ucBtnCancel";
            this.ucBtnCancel.Size = new System.Drawing.Size(88, 22);
            this.ucBtnCancel.TabIndex = 2;
            this.ucBtnCancel.Click += new System.EventHandler(this.ucBtnCancel_Click);
            // 
            // ucLERunningCard
            // 
            this.ucLERunningCard.AllowEditOnlyChecked = true;
            this.ucLERunningCard.AutoSelectAll = false;
            this.ucLERunningCard.AutoUpper = true;
            this.ucLERunningCard.Caption = "输入框";
            this.ucLERunningCard.Checked = false;
            this.ucLERunningCard.EditType = UserControl.EditTypes.String;
            this.ucLERunningCard.Location = new System.Drawing.Point(19, 19);
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
            this.ucLERunningCard.XAlign = 68;
            this.ucLERunningCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLERunningCard_TxtboxKeyPress);
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(413, 52);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 4;
            // 
            // ucBtnOK
            // 
            this.ucBtnOK.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnOK.BackgroundImage")));
            this.ucBtnOK.ButtonType = UserControl.ButtonTypes.Confirm;
            this.ucBtnOK.Caption = "确认";
            this.ucBtnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnOK.Location = new System.Drawing.Point(93, 52);
            this.ucBtnOK.Name = "ucBtnOK";
            this.ucBtnOK.Size = new System.Drawing.Size(88, 22);
            this.ucBtnOK.TabIndex = 1;
            this.ucBtnOK.Click += new System.EventHandler(this.ucBtnOK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.aCardTransLetterULE);
            this.panel1.Controls.Add(this.aCardTransLenULE);
            this.panel1.Controls.Add(this.bCardTransLenULE);
            this.panel1.Controls.Add(this.checkMO);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.bCardTransLetterULE);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(785, 104);
            this.panel1.TabIndex = 1;
            // 
            // aCardTransLetterULE
            // 
            this.aCardTransLetterULE.AllowEditOnlyChecked = true;
            this.aCardTransLetterULE.AutoSelectAll = false;
            this.aCardTransLetterULE.AutoUpper = true;
            this.aCardTransLetterULE.Caption = "转换后序列号首字符串";
            this.aCardTransLetterULE.Checked = false;
            this.aCardTransLetterULE.EditType = UserControl.EditTypes.String;
            this.aCardTransLetterULE.Location = new System.Drawing.Point(343, 66);
            this.aCardTransLetterULE.MaxLength = 40;
            this.aCardTransLetterULE.Multiline = false;
            this.aCardTransLetterULE.Name = "aCardTransLetterULE";
            this.aCardTransLetterULE.PasswordChar = '\0';
            this.aCardTransLetterULE.ReadOnly = false;
            this.aCardTransLetterULE.ShowCheckBox = true;
            this.aCardTransLetterULE.Size = new System.Drawing.Size(282, 24);
            this.aCardTransLetterULE.TabIndex = 20;
            this.aCardTransLetterULE.TabNext = false;
            this.aCardTransLetterULE.Value = "";
            this.aCardTransLetterULE.WidthType = UserControl.WidthTypes.Normal;
            this.aCardTransLetterULE.XAlign = 492;
            this.aCardTransLetterULE.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.aCardTransLetterULE_TxtboxKeyPress);
            // 
            // aCardTransLenULE
            // 
            this.aCardTransLenULE.AllowEditOnlyChecked = true;
            this.aCardTransLenULE.AutoSelectAll = false;
            this.aCardTransLenULE.AutoUpper = true;
            this.aCardTransLenULE.Caption = "转换后序列号长度";
            this.aCardTransLenULE.Checked = false;
            this.aCardTransLenULE.EditType = UserControl.EditTypes.String;
            this.aCardTransLenULE.Location = new System.Drawing.Point(37, 66);
            this.aCardTransLenULE.MaxLength = 40;
            this.aCardTransLenULE.Multiline = false;
            this.aCardTransLenULE.Name = "aCardTransLenULE";
            this.aCardTransLenULE.PasswordChar = '\0';
            this.aCardTransLenULE.ReadOnly = false;
            this.aCardTransLenULE.ShowCheckBox = true;
            this.aCardTransLenULE.Size = new System.Drawing.Size(258, 24);
            this.aCardTransLenULE.TabIndex = 21;
            this.aCardTransLenULE.TabNext = false;
            this.aCardTransLenULE.Value = "";
            this.aCardTransLenULE.WidthType = UserControl.WidthTypes.Normal;
            this.aCardTransLenULE.XAlign = 162;
            // 
            // bCardTransLenULE
            // 
            this.bCardTransLenULE.AllowEditOnlyChecked = true;
            this.bCardTransLenULE.AutoSelectAll = false;
            this.bCardTransLenULE.AutoUpper = true;
            this.bCardTransLenULE.Caption = "转换前序列号长度";
            this.bCardTransLenULE.Checked = false;
            this.bCardTransLenULE.EditType = UserControl.EditTypes.String;
            this.bCardTransLenULE.Location = new System.Drawing.Point(37, 38);
            this.bCardTransLenULE.MaxLength = 40;
            this.bCardTransLenULE.Multiline = false;
            this.bCardTransLenULE.Name = "bCardTransLenULE";
            this.bCardTransLenULE.PasswordChar = '\0';
            this.bCardTransLenULE.ReadOnly = false;
            this.bCardTransLenULE.ShowCheckBox = true;
            this.bCardTransLenULE.Size = new System.Drawing.Size(258, 24);
            this.bCardTransLenULE.TabIndex = 22;
            this.bCardTransLenULE.TabNext = false;
            this.bCardTransLenULE.Value = "";
            this.bCardTransLenULE.WidthType = UserControl.WidthTypes.Normal;
            this.bCardTransLenULE.XAlign = 162;
            // 
            // checkMO
            // 
            this.checkMO.AllowEditOnlyChecked = true;
            this.checkMO.AutoSelectAll = false;
            this.checkMO.AutoUpper = true;
            this.checkMO.Caption = "工单检查        ";
            this.checkMO.Checked = false;
            this.checkMO.EditType = UserControl.EditTypes.String;
            this.checkMO.Location = new System.Drawing.Point(37, 10);
            this.checkMO.MaxLength = 40;
            this.checkMO.Multiline = false;
            this.checkMO.Name = "checkMO";
            this.checkMO.PasswordChar = '\0';
            this.checkMO.ReadOnly = false;
            this.checkMO.ShowCheckBox = true;
            this.checkMO.Size = new System.Drawing.Size(258, 24);
            this.checkMO.TabIndex = 15;
            this.checkMO.TabNext = true;
            this.checkMO.Value = "";
            this.checkMO.WidthType = UserControl.WidthTypes.Normal;
            this.checkMO.XAlign = 162;
            this.checkMO.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.checkMO_TxtboxKeyPress);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 104);
            this.splitter1.TabIndex = 13;
            this.splitter1.TabStop = false;
            // 
            // bCardTransLetterULE
            // 
            this.bCardTransLetterULE.AllowEditOnlyChecked = true;
            this.bCardTransLetterULE.AutoSelectAll = false;
            this.bCardTransLetterULE.AutoUpper = true;
            this.bCardTransLetterULE.Caption = "转换前序列号首字符串";
            this.bCardTransLetterULE.Checked = false;
            this.bCardTransLetterULE.EditType = UserControl.EditTypes.String;
            this.bCardTransLetterULE.Location = new System.Drawing.Point(343, 38);
            this.bCardTransLetterULE.MaxLength = 40;
            this.bCardTransLetterULE.Multiline = false;
            this.bCardTransLetterULE.Name = "bCardTransLetterULE";
            this.bCardTransLetterULE.PasswordChar = '\0';
            this.bCardTransLetterULE.ReadOnly = false;
            this.bCardTransLetterULE.ShowCheckBox = true;
            this.bCardTransLetterULE.Size = new System.Drawing.Size(282, 24);
            this.bCardTransLetterULE.TabIndex = 19;
            this.bCardTransLetterULE.TabNext = false;
            this.bCardTransLetterULE.Value = "";
            this.bCardTransLetterULE.WidthType = UserControl.WidthTypes.Normal;
            this.bCardTransLetterULE.XAlign = 492;
            // 
            // ucMessage
            // 
            this.ucMessage.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessage.ButtonVisible = false;
            this.ucMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessage.Location = new System.Drawing.Point(0, 104);
            this.ucMessage.Name = "ucMessage";
            this.ucMessage.Size = new System.Drawing.Size(785, 392);
            this.ucMessage.TabIndex = 2;
            // 
            // FCollectionIDMerge
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(785, 586);
            this.Controls.Add(this.ucMessage);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FCollectionIDMerge";
            this.Text = "序列号转换采集";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FCollectionIDMerge_Load);
            this.Closed += new System.EventHandler(this.FCollectionIDMerge_Closed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region Button Events
		private void ucBtnOK_Click(object sender, System.EventArgs e)
		{
			if ( this.ucLERunningCard.Value.Trim() == string.Empty )
			{
				ucLERunningCard.TextFocus(true, true);
				return;
			}	

			this.ucMessage.Add(string.Format("<< {0}", this.ucLERunningCard.Value.Trim().ToUpper() ));

            //转换成起始序列号 Add By Bernard @ 2010-10-29
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceCard = dataCollectFacade.GetSourceCard(this.ucLERunningCard.Value.Trim().ToUpper(), string.Empty);
            //end

            //Add By Bernard @ 2010-10-29 for Check产品序列号Hold
            //Simulation objSimulation = dataCollectFacade.GetLastSimulation(sourceCard) as Simulation;
            //if (objSimulation != null)
            //{
            //    if (dataCollectFacade.CheckRcardHold(sourceCard, objSimulation.MOCode.ToUpper().Trim()))
            //    {
            //        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$RACRD_HOLD $CS_Param_ID=" + sourceCard));
            //        this.ucLERunningCard.Value = "";
            //        ucLERunningCard.TextFocus(true, true);
            //        return;
            //    }
            //}
            //end 
			
	        //Modified By Bernard @ 2010-10-29
			//Messages messages = this._helper.GetIDInfo(this.ucLERunningCard.Value.Trim().ToUpper());
            Messages messages = this._helper.GetIDInfo(sourceCard);

			if ( !messages.IsSuccess() )
			{
				this.ucMessage.Add( messages );
				this.ucLERunningCard.Value = "";

				//Laws Lu,2005/08/11,新增焦点设置
				ucLERunningCard.TextFocus(true, true);
					
				return;
			}
		
			/* added by jessie lee, 2005/12/10 
			 * 判断 */
			bool isSameMO = false;
			bool updateSim = false ;
			decimal existIMEISeq = 0 ;

			// 输入分板前产品序列号
			if ( this._currSequence == 0 )
			{
                //Add By Bernard @ 2010-10-29
                _tCard = this.ucLERunningCard.Value.Trim().ToUpper();

				//Laws Lu,2005/10/19,新增	缓解性能问题
				//Laws Lu,2006/12/25 修改	减少Open/Close的次数
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
				//added by jessie lee, 2005/11/29
				#region 判断转换前序列号是否符合条件
				//长度检查
				if( bCardTransLenULE.Checked )
				{
					if( bCardTransLenULE.Value.Trim().Length == 0 )
					{
						this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_Before_Card_Transfer_Empty")); 
						ucLERunningCard.TextFocus(true, true);
						return ;
					}

					int len = 0;
					try
					{
						len = int.Parse(bCardTransLenULE.Value.Trim());
					}
					catch
					{
						this.ucMessage.Add( new UserControl.Message(MessageType.Error,"$Error_BeforeCardTransLen_Should_be_Integer"));
						ucLERunningCard.TextFocus(true, true);
						return ;
					}

					if( len != this.ucLERunningCard.Value.Trim().Length )
					{
						this.ucMessage.Add( new UserControl.Message(MessageType.Error,"$Error_BeforeCardTransLen_Not_Correct"));
						ucLERunningCard.TextFocus(true, true);
						return ;
					}
				}

				//首字符串检查
				if(bCardTransLetterULE.Checked)
				{
					if( bCardTransLetterULE.Value.Trim().Length == 0 )
					{
						this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_Before_Card_Transfer_FLetter_Empty")); 
						ucLERunningCard.TextFocus(true, true);
						return ;
					}

					int index = ucLERunningCard.Value.Trim().IndexOf( bCardTransLetterULE.Value.Trim() );
					if( index != 0 )
					{
						this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_Before_Card_Transfer_FLetter_NotCompare")); 
						ucLERunningCard.TextFocus(true, true);
						return ;
					}
				}
				#endregion

				#region 取得自定义分板比例 (Has Comment Out)
                /*Comment Out By Bernard @ 2010-10-29
				if ( this.ucLEIDMergeRule.Checked )
				{
					if ( this.ucLEIDMergeRule.Value.Trim() == string.Empty )
					{
						this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_Please_Input_IDMerge_Rule"));		//请输入自定义分板比例

						//Laws Lu,2005/08/11,新增焦点设置
						ucLERunningCard.TextFocus(true, true);
						return;
					}
				
					int mergeRule = 1;
					
					try
					{
						mergeRule = System.Int32.Parse( this.ucLEIDMergeRule.Value.Trim() );
					}
					catch
					{
						this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$Error_CS_IDMerge_Should_be_Integer"));//分板比例必须为整数

						//Laws Lu,2005/08/11,新增焦点设置
						ucLERunningCard.TextFocus(true, true);
						return;
					}

					if ( mergeRule <= 0 )
					{
						this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$Error_CS_IDMerge_Should_Over_Zero"));//分板比例必须大于零

						//Laws Lu,2005/08/11,新增焦点设置
						ucLERunningCard.TextFocus(true, true);
						return;
					}

					this._mergeRule = mergeRule;
				}
                 * */
				#endregion

				#region 判断分板前序列号是否存在
				this._product= (ProductInfo)messages.GetData().Values[0];

                if (this._product.LastSimulation == null)
                {
                    // Added by Icyer 2006/11/08
                    // Undo时，无法取道已转换的序列号Simulation，需要用TCARD找转换后序列号
                    bool bNotExist = true;
                    if (this.chkUndo.Checked == true)
                    {
                        DataCollectFacade dcFacade = new DataCollectFacade(this.DataProvider);
                        transedRunningCardByProduct = dcFacade.GetSimulationFromTCard(this.ucLERunningCard.Value.Trim().ToUpper());
                        if (transedRunningCardByProduct != null && transedRunningCardByProduct.Length > 0)
                        {
                            this._product = this._helper.GetIDInfoBySimulation((Simulation)transedRunningCardByProduct[0]);
                            this._product.LastSimulation.RunningCard = this.ucLERunningCard.Value.Trim().ToUpper();
                            this._product.NowSimulation = (Simulation)this._product.LastSimulation;
                            bNotExist = false;
                        }
                    }
                    // Added end
                    if (bNotExist == true)
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$Error_CS_ID_Not_Exist"));//序列号不存在
                        this.ucLERunningCard.Value = "";

                        //Laws Lu,2005/08/11,新增焦点设置
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }
                }
                else if (this.chkUndo.Checked == true)	// Added by Icyer 2006/11/08
                {
                    this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$IDMerge_Undo_RunningCard_OnLine"));
                    this.ucLERunningCard.Value = "";
                    ucLERunningCard.TextFocus(true, true);
                    return;
                }

				//added by jessie lee, 2006/7/21
				#region 序列号转换增加工单防呆功能

                if (checkMO.Checked
                    && string.Compare(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(checkMO.Value)), this._product.LastSimulation.MOCode, true) != 0)
                {
                    this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$Error_MO_Not_Match"));//序列号不存在
                    this.ucLERunningCard.Value = "";

                    //Laws Lu,2005/08/11,新增焦点设置
                    ucLERunningCard.TextFocus(true, true);
                    return;
                }
 
				#endregion

				// Added by Icyer 2006/11/08
				// 只有正常采集时才调用CheckID检查途程
                if (this.chkUndo.Checked == false)
                {
                    messages = new DataCollectFacade(this.DataProvider).CheckID(
                        this._product.LastSimulation.RunningCard,
                        ActionType.DataCollectAction_Split,
                        ApplicationService.Current().ResourceCode,
                        ApplicationService.Current().UserCode,
                        this._product);
                }

				if ( !messages.IsSuccess() )
				{
					this.ucMessage.Add( messages );
					this.ucLERunningCard.Value = "";

					//Laws Lu,2005/08/11,新增焦点设置
					ucLERunningCard.TextFocus(true, true);
					return;
				}
				#endregion

				#region 判断是否分板工序，取系统分板比例
                object op = new ItemFacade(this.DataProvider).GetItemRoute2Operation(this._product.NowSimulation.ItemCode,
                    this._product.NowSimulation.RouteCode,
                    this._product.NowSimulation.OPCode);

                if (op == null)
                {
                    this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$Error_CS_Current_OP_Not_Exist"));//当前工序不存在
                    this.ucLERunningCard.Value = "";

                    //Laws Lu,2005/08/11,新增焦点设置
                    ucLERunningCard.TextFocus(true, true);

                    return;
                }

                if (((ItemRoute2OP)op).OPControl[(int)OperationList.IDTranslation] != '1')
                {
                    this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_OP_Not_SplitOP"));//当前工序不是序号转换工序
                    this.ucLERunningCard.Value = "";

                    //Laws Lu,2005/08/11,新增焦点设置
                    ucLERunningCard.TextFocus(true, true);

                    return;
                }

                #region Comment Out By Bernard @ 2010-10-29

                // 取系统分板比例
				//if ( ((ItemRoute2OP)op).IDMergeType == IDMergeType.IDMERGETYPE_ROUTER )
				//{
					// Removed by Icyer 2006/11/08
					/*
					//AMOI  MARK  START  20050804 大板序号第一位必须为D  小板序号第一位必须为B 
					if  (this.ucLERunningCard.Value[0]!='D')
					{
						this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_theFirstOfBIGID_MustBe_D"));
						this.ucLERunningCard.Value = "";	
				
						//Laws Lu,2005/08/11,新增焦点设置
						ucLERunningCard.TextFocus(true, true);
						return;
					}
					//AMOI  MARK  END
					*/
                
					// 取当前工序的分板比例
			/* Comment Out		if ( !this.ucLEIDMergeRule.Checked )
					{
						this._mergeRule = (int)((ItemRoute2OP)op).IDMergeRule;
					}
				}
				else
				{
					this._mergeRule = 1;
                }
             *Comment Out */

                #endregion Comment Out
                //Add By Bernard @2010-10-29
                this._mergeRule=1;

                #endregion

                // 取序号转换类型
				this._idMergeType = ((ItemRoute2OP)op).IDMergeType;
				this._runningCardList = new ArrayList( this._mergeRule );
				// Added by Icyer 2006/11/08, 如果是Undo，则自动带出原转换后序列号
				if (this.chkUndo.Checked == true)
				{
					// 检查序列号是否还在当前工序
					if (this._product.LastSimulation.ResourceCode != Service.ApplicationService.Current().ResourceCode)
					{
						object objTmp = (new BaseSetting.BaseModelFacade(this.DataProvider)).GetOperation2Resource(this._product.LastSimulation.OPCode, Service.ApplicationService.Current().ResourceCode);
						if (objTmp == null)
						{
							this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$IDMerge_Undo_CurrentOP_Error"));
							ucLERunningCard.TextFocus(true, true);
							return;
						}
					}
					// 显示原转换后序列号列表
					if (transedRunningCardByProduct != null && transedRunningCardByProduct.Length > 0)
					{
						this.ucMessage.AddBoldText("$IDMerge_Undo_Old_RunningCard_List:");
						for (int i = 0; i < transedRunningCardByProduct.Length; i++)
						{
                            //Modified By Bernard @ 2010-10-29
							//this.ucMessage.Add(((Simulation)transedRunningCardByProduct[i]).RunningCard);
                            this.ucMessage.Add(((Simulation)transedRunningCardByProduct[i]).TranslateCard);
						}
					}
				}
				// Added end
			}
			else
			{
				#region 该逻辑已经停用, jessie lee, 2005/11/29

				//AMOI  MARK  START  20050804 大板序号第一位必须为D  小板序号第一位必须为B 
//				if (this._idMergeType == IDMergeType.IDMERGETYPE_ROUTER )
//				{
//					if  (this.ucLERunningCard.Value.Trim().ToUpper()[0]!='B')
//					{
//						this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_theFirstOfSmallID_MustBe_B"));
//						this.ucLERunningCard.Value = "";					
//						Laws Lu,2005/08/11,新增焦点设置
//						ucLERunningCard.TextFocus(true, true);
//						return;
//					}
//				}
				//AMOI  MARK  END
				#endregion

				//added by jessie lee, 2005/11/29
				#region 判断转换后序列号是否符合条件
				//长度检查
				if( aCardTransLenULE.Checked )
				{
                    if (aCardTransLenULE.Value.Trim().Length == 0)
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_After_Card_Transfer_Empty"));
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }

					int len = 0;
                    try
                    {
                        len = int.Parse(aCardTransLenULE.Value.Trim());
                    }
                    catch
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$Error_AfterCardTransLen_Should_be_Integer"));
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }

					if( len != this.ucLERunningCard.Value.Trim().Length )
					{
						this.ucMessage.Add( new UserControl.Message(MessageType.Error,"$Error_AfterCardTransLen_Not_Correct"));
						ucLERunningCard.TextFocus(true, true);
						return ;
					}
				}

				//首字符串检查
				if(aCardTransLetterULE.Checked)
				{
					if( aCardTransLetterULE.Value.Trim().Length == 0 )
					{
						this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_After_Card_Transfer_FLetter_Empty")); 
						ucLERunningCard.TextFocus(true, true);
						return ;
					}

					int index = ucLERunningCard.Value.Trim().IndexOf( aCardTransLetterULE.Value.Trim() );
					if( index != 0 )
					{
						this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_After_Card_Transfer_FLetter_NotCompare")); 
						ucLERunningCard.TextFocus(true, true);
						return ;
					}
				}
				#endregion

				// 判断分板后序列号是否已存在

                if (((ProductInfo)messages.GetData().Values[0]).LastSimulation != null)
                {
                    /* modified by jessie lee, 2005/12/9, CS188-3
                     * 1,包装段的产品被拆解或报废后，可能再次投入到包装段，但是由于EMP平台的手机无法使用新的IMEI号，
                     * 就存在着重用IMEI号的问题，而且该IMEI号码有可能投入到原来的工单。顾问给客户的解决方案是不良拆
                     * 解或报废后的手机再次投入包装时粘贴新的M条码（IMEI序号转换前的号码），该号码可以归属到原来的工
                     * 单或新工单，序号转换时系统允许将M条码转换成曾经使用过的IMEI号
                     * 2,RMA等客诉返工的EMP平台的产品返工生产时同样可能存在重复使用IMEI号的问题。顾问给出的第一种解
                     * 决方式是开立返工类型的工单，以IMEI号投入新的返工工单，开始生产活动，但是新的生产流程不需要包
                     * 涵序号转换工序；第二种方式是粘贴新的M条码投入到正常工单中，序号转换时，如果出现IMEI号码重复使
                     * 用则必须保证原来使用IMEI号的工单已经关单。客户要求采用第二种方法。 
                     * 
                     * 2中客户要求采用第二种方法，那么采集进来的rcard是没有simulation记录的，所以不会进入这里check
                     * */
                    /* 不是序列号转换工序，保持原来的逻辑 */
                    if (string.Compare(this._idMergeType, IDMergeType.IDMERGETYPE_IDMERGE, true) != 0)
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$Error_CS_ID_Already_Exist"));//序列号已存在
                        this.ucLERunningCard.Value = "";

                        //Laws Lu,2005/08/11,新增焦点设置
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }

                    /* 是序列号转换工序 
                     * 转换前的rcard 和 转换后的rcard  不相同
                     * 不同， check IMEI 重复使用
                     * */
                    if (string.Compare(this._product.LastSimulation.RunningCard, this.ucLERunningCard.Value.Trim(), true) != 0)
                    {
                        string bMoCode = this._product.LastSimulation.MOCode;
                        string aMoCode = ((ProductInfo)messages.GetData().Values[0]).LastSimulation.MOCode;

                        /* 判断这个IMEI号是否报废或者拆解 */
                        bool isSpliteOrScrape = CheckIMEISpliteOrScrape(
                            ((ProductInfo)messages.GetData().Values[0]).LastSimulation.RunningCard,
                            ((ProductInfo)messages.GetData().Values[0]).LastSimulation.RunningCardSequence,
                            aMoCode);
                        if (!isSpliteOrScrape)
                        {
                            /* rcard 完工，工单未关 */
                            if (((ProductInfo)messages.GetData().Values[0]).LastSimulation.IsComplete != "1"
                                && ((ProductInfo)messages.GetData().Values[0]).LastSimulation.ProductStatus != ProductStatus.OffMo)
                            {
                                this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$Error_CS_ID_Already_Exist"));//序列号已存在
                                this.ucLERunningCard.Value = "";

                                //Laws Lu,2005/08/11,新增焦点设置
                                ucLERunningCard.TextFocus(true, true);
                                return;
                            }
                        }

                        /* 归属同一张工单 */
                        if (string.Compare(bMoCode, aMoCode, true) == 0)
                        {
                            isSameMO = true;
                            //existIMEISeq = ((ProductInfo)messages.GetData().Values[0]).LastSimulation.RunningCardSequence ;

                        }
                        else
                        {
                            /* 归属不同工单 */
                            isSameMO = false;

                        }
                        existIMEISeq = ((ProductInfo)messages.GetData().Values[0]).LastSimulation.RunningCardSequence;
                        updateSim = true;

                    }
                    else /* rcard == tcard */
                    {
                        isSameMO = true;
                        existIMEISeq = ((ProductInfo)messages.GetData().Values[0]).LastSimulation.RunningCardSequence;
                    }
                }

				// 判断分板后序列号是否重复
				if ( this._runningCardList.Contains(this.ucLERunningCard.Value.Trim().ToUpper()) ) 
				{
					this.ucMessage.Add(new UserControl.Message(MessageType.Error,"$Error_CS_Merge_ID_Exist"));//转换后产品序列号重复
					this.ucLERunningCard.Value = "";
					
					//Laws Lu,2005/08/11,新增焦点设置
					ucLERunningCard.TextFocus(true, true);
					return;
				}

				this._runningCardList.Add( this.ucLERunningCard.Value.Trim().ToUpper() );
			}

			if ( this._currSequence < this._mergeRule )
			{
				this._currSequence++;

                //Comment Out By Bernard @ 2010-10-29
				//this.ucLEIDMergeRule.Enabled = false;
				this.ucMessage.AddBoldText( string.Format(">>$CS_Please_Input_Merge_ID {0}/{1}", this._currSequence.ToString(), this._mergeRule.ToString()));//请输入转换后产品序列号
			}
			else if ( this._currSequence == this._mergeRule ) // 达到分板比例,写入数据库
			{
		
				messages = this.doAction(isSameMO, int.Parse(existIMEISeq.ToString()), updateSim);

				if ( messages.IsSuccess() )	// 成功
				{
					this.ucMessage.Add(new UserControl.Message(MessageType.Success,">>$CS_SplitID_CollectSuccess"));//产品序列号转换采集成功
					//added by jessie lee, 2005/11/29,
					#region 添加计数功能
					int count = int.Parse(this.CollectedCount.Text)+1;
					this.CollectedCount.Text = count.ToString();
					#endregion
				}
				else						// 失败
				{
					this.ucMessage.Add( messages );
				}

				this.initInput();
			}

			this.ucLERunningCard.Value = "";

			//Laws Lu,2005/08/11,新增焦点设置
			if(!aCardTransLetterULE.Checked)
			{
				ucLERunningCard.TextFocus(true, true);

				//SendKeys.Send("+{TAB}");
			}
			/*	Removed by Icyer 2006/12/11
			else if(this._currSequence == 0 )
			{
				aCardTransLetterULE.TextFocus(true, true);
			}
			*/
			else
			{
				ucLERunningCard.TextFocus(true, true);
			}
		}

		private void ucBtnCancel_Click(object sender, System.EventArgs e)
		{
			this.initInput();
		}

        private void ucBtnRecede_Click(object sender, System.EventArgs e)
        {
            if (this._currSequence > 0)
            {
                if (this._runningCardList.Count > 0)
                {
                    this._runningCardList.RemoveAt(this._runningCardList.Count - 1);
                }
                this._currSequence--;

                if (this._currSequence > 0)
                {
                    this.ucMessage.Add(string.Format(">>$CS_Please_Input_Merge_ID {0}/{1}", this._currSequence.ToString(), this._mergeRule.ToString()));
                }
                else
                {
                    this.ucMessage.Add(">>$CS_Please_Input_ID_To_Merge");//请输入转换前产品序列号
                    //Comment Out By Bernard @ 2010-10-29 
                    //this.ucLEIDMergeRule.Enabled = true;
                }
            }

            //Laws Lu,2005/08/11,新增焦点设置
            ucLERunningCard.TextFocus(true, true);
        }
		#endregion

		#region Events
		public void ucLERunningCard_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar == '\r')
			{
				this.ucBtnOK_Click( sender, null );

				//Laws Lu,2005/08/11,新增焦点设置,注释
				//SendKeys.Send("+{TAB}");
			}
		}

		private void FCollectionIDMerge_Load(object sender, System.EventArgs e)
		{
			this.initInput();
            //this.InitPageLanguage();
		}
		#endregion

		#region Function
		private void initInput()
		{
			this.chkUndo.Checked = false;	// Added by Icyer 2006/11/08
			this._currSequence = 0;
            //Commemt Out By Bernard @ 2010-10-29
			//this.ucLEIDMergeRule.Enabled = true;
			this.ucMessage.Add(">>$CS_Please_Input_ID_To_Merge");

			this.ucLERunningCard.TextFocus(false, true);
		}

		private Messages doAction(bool IsSameMO, int ExistIMEISeq, bool UpdateSimulation)
		{
			
			Messages messages = new Messages();

            /* Comment Out By Bernard @ 2010-10-29
			SplitIDActionEventArgs args = new SplitIDActionEventArgs(
				ActionType.DataCollectAction_Split, 
				this._product.LastSimulation.RunningCard, 
				ApplicationService.Current().UserCode,
				ApplicationService.Current().ResourceCode,
				this._product, 
				(object[])this._runningCardList.ToArray(),
				this._idMergeType,
				IsSameMO,
				ExistIMEISeq,
				UpdateSimulation);
             * */

            //Add By Bernard @ 2010-10-29
            ConvertCardActionEventArgs args = new ConvertCardActionEventArgs(
                ActionType.DataCollectAction_Convert,
                _product.LastSimulation.RunningCard,
                _tCard,
               ApplicationService.Current().UserCode,
                 ApplicationService.Current().ResourceCode,
                _product,
                _runningCardList[0].ToString(),
                this._idMergeType,
                IsSameMO,
                ExistIMEISeq,
                true);

			// Added by Icyer 2006/11/08
			if (this.chkUndo.Checked == true)
			{
				args.IsUndo = true;
			}
			// Added end

            //Modified By Bernard @ 2010-10-29
			//IAction action = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_Split);
            IAction action = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_Convert);
	
			//Laws Lu,2005/10/19,新增	缓解性能问题
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			DataProvider.BeginTransaction();
			try
			{
				
				messages.AddMessages(action.Execute(args));	

				if ( messages.IsSuccess() )
				{
					this.DataProvider.CommitTransaction();
					messages.Add( new UserControl.Message(MessageType.Success,"$CS_SplitID_CollectSuccess") );
				}
				else
				{
					this.DataProvider.RollbackTransaction();
				}

				return messages;
			}
			catch(Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				throw ex;
			}
			finally
			{
				//Laws Lu,2005/10/19,新增	缓解性能问题
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
			}
		}
		#endregion

		private void FCollectionIDMerge_Closed(object sender, System.EventArgs e)
		{
			if (this.DataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();  
		}

		private void panel2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		/// <summary>
		/// 检查当前rcard是否拆解或则报废
		/// </summary>
		/// <param name="rcard"></param>
		/// <param name="rcardseq"></param>
		/// <param name="mocode"></param>
		/// <returns></returns>
        private bool CheckIMEISpliteOrScrape(string rcard, decimal rcardseq, string mocode)
        {
            string sql = string.Format(" select count(*) from tblts where rcard='{0}' and rcardseq={1} and mocode='{2}' and tsstatus in ('{3}','{4}')",
                rcard, rcardseq, mocode, TSStatus.TSStatus_Scrap, TSStatus.TSStatus_Split);
            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count > 0)
            {
                return true;
            }
            return false;
        }

		private void aCardTransLetterULE_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				ucLERunningCard.TextFocus(true, true);
			}
		}

        //Method Add by Bernard @ 2010-10-29
        private void checkMO_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.ucLERunningCard.TextFocus(false, true);
            }
        }

	}
}
