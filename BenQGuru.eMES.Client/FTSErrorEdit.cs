using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.TS;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Web.Helper;

using UserControl;

namespace BenQGuru.eMES.Client
{
	public class FTSErrorEdit : BaseForm
    {
        #region 变量

        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.GroupBox groupBoxItemInfoForTS;
        private System.Windows.Forms.GroupBox groupBoxDown;        
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblUnselectedErrorCode;
        private System.Windows.Forms.Label lblSelectedErrorCode;
		
		private UserControl.UCLabelEdit ucLabEditRunningCard;
        private UserControl.UCLabelCombox ucLabelComboxErrorGroup;
		private UserControl.UCButton btnSave;
		private UserControl.UCButton ucButtonExit;
        private UserControl.UCButton ucButtonAdd;
        private UserControl.UCButton ucButtonRemove;				
		private System.Windows.Forms.ListView listViewSelect;
		private System.Windows.Forms.ListView listViewSelected;

        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;
        private ListChangeHelper<TSErrorCode> _ListHelper = new ListChangeHelper<TSErrorCode>();

        private string _RunningCardTitle = string.Empty;
        private bool _AddTSErrorCodeWhenSave = false;
        private Parameter _DefaultErrorCode = null;
        private Domain.TS.TS _CurrentTS = null;
        private TSErrorCode[] _SelectedTSErrorCode = null;

        private TSFacade _TSFacade = null;
        private TSModelFacade _TSModelFacade = null;        

        #endregion

        #region 属性

        public IDomainDataProvider DataProvider
		{
			get
			{
				return _DomainDataProvider;
			}
        }

        public string RunningCardTitle
		{
			get
			{
				return _RunningCardTitle;
			}
			set
			{
				_RunningCardTitle = value;
			}
		}

        public bool AddTSErrorCodeWhenSave
        {
            get
            {
                return _AddTSErrorCodeWhenSave;
            }
            set
            {
                _AddTSErrorCodeWhenSave = value;
            }
        }

        public Domain.TS.TS CurrentTS
		{
			get
			{
				return _CurrentTS;
			}
			set
			{
				_CurrentTS = value;				
			}
		}

        public TSErrorCode[] SelectedTSErrorCode
        {
            get
            {
                return _SelectedTSErrorCode;
            }
            set
            {
                _SelectedTSErrorCode = value;
            }
        }

        #endregion

        #region 辅助

        protected void ShowMessage(string message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        protected void ShowMessage(UserControl.Message message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        protected void ShowMessage(Exception e)
        {
            this.ShowMessage(new UserControl.Message(e));
        }

        #endregion

        #region 基本

        public FTSErrorEdit()
		{
			InitializeComponent();

			UserControl.UIStyleBuilder.FormUI(this);	

			_TSFacade = new TSFacade( this.DataProvider );
			_TSModelFacade = new TSModelFacade( this.DataProvider );
		}

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

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTSErrorEdit));
            this.groupBoxItemInfoForTS = new System.Windows.Forms.GroupBox();
            this.ucLabEditRunningCard = new UserControl.UCLabelEdit();
            this.groupBoxDown = new System.Windows.Forms.GroupBox();
            this.btnSave = new UserControl.UCButton();
            this.ucButtonExit = new UserControl.UCButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listViewSelected = new System.Windows.Forms.ListView();
            this.listViewSelect = new System.Windows.Forms.ListView();
            this.lblSelectedErrorCode = new System.Windows.Forms.Label();
            this.lblUnselectedErrorCode = new System.Windows.Forms.Label();
            this.ucButtonAdd = new UserControl.UCButton();
            this.ucButtonRemove = new UserControl.UCButton();
            this.ucLabelComboxErrorGroup = new UserControl.UCLabelCombox();
            this.groupBoxItemInfoForTS.SuspendLayout();
            this.groupBoxDown.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxItemInfoForTS
            // 
            this.groupBoxItemInfoForTS.Controls.Add(this.ucLabEditRunningCard);
            this.groupBoxItemInfoForTS.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxItemInfoForTS.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxItemInfoForTS.Location = new System.Drawing.Point(0, 0);
            this.groupBoxItemInfoForTS.Name = "groupBoxItemInfoForTS";
            this.groupBoxItemInfoForTS.Size = new System.Drawing.Size(514, 48);
            this.groupBoxItemInfoForTS.TabIndex = 165;
            this.groupBoxItemInfoForTS.TabStop = false;
            this.groupBoxItemInfoForTS.Text = "产品信息";
            // 
            // ucLabEditRunningCard
            // 
            this.ucLabEditRunningCard.AllowEditOnlyChecked = true;
            this.ucLabEditRunningCard.AutoSelectAll = false;
            this.ucLabEditRunningCard.AutoUpper = true;
            this.ucLabEditRunningCard.Caption = "产品序列号";
            this.ucLabEditRunningCard.Checked = false;
            this.ucLabEditRunningCard.EditType = UserControl.EditTypes.String;
            this.ucLabEditRunningCard.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucLabEditRunningCard.Location = new System.Drawing.Point(17, 16);
            this.ucLabEditRunningCard.MaxLength = 40;
            this.ucLabEditRunningCard.Multiline = false;
            this.ucLabEditRunningCard.Name = "ucLabEditRunningCard";
            this.ucLabEditRunningCard.PasswordChar = '\0';
            this.ucLabEditRunningCard.ReadOnly = true;
            this.ucLabEditRunningCard.ShowCheckBox = false;
            this.ucLabEditRunningCard.Size = new System.Drawing.Size(273, 24);
            this.ucLabEditRunningCard.TabIndex = 0;
            this.ucLabEditRunningCard.TabNext = true;
            this.ucLabEditRunningCard.Value = "";
            this.ucLabEditRunningCard.WidthType = UserControl.WidthTypes.Long;
            this.ucLabEditRunningCard.XAlign = 90;
            // 
            // groupBoxDown
            // 
            this.groupBoxDown.Controls.Add(this.btnSave);
            this.groupBoxDown.Controls.Add(this.ucButtonExit);
            this.groupBoxDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxDown.Location = new System.Drawing.Point(0, 398);
            this.groupBoxDown.Name = "groupBoxDown";
            this.groupBoxDown.Size = new System.Drawing.Size(514, 56);
            this.groupBoxDown.TabIndex = 292;
            this.groupBoxDown.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.ButtonType = UserControl.ButtonTypes.Save;
            this.btnSave.Caption = "保存";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(144, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 22);
            this.btnSave.TabIndex = 11;
            this.btnSave.Click += new System.EventHandler(this.ucButtonSave_Click);
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(280, 16);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listViewSelected);
            this.panel1.Controls.Add(this.listViewSelect);
            this.panel1.Controls.Add(this.lblSelectedErrorCode);
            this.panel1.Controls.Add(this.lblUnselectedErrorCode);
            this.panel1.Controls.Add(this.ucButtonAdd);
            this.panel1.Controls.Add(this.ucButtonRemove);
            this.panel1.Controls.Add(this.ucLabelComboxErrorGroup);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(514, 350);
            this.panel1.TabIndex = 293;
            // 
            // listViewSelected
            // 
            this.listViewSelected.LabelWrap = false;
            this.listViewSelected.Location = new System.Drawing.Point(312, 64);
            this.listViewSelected.Name = "listViewSelected";
            this.listViewSelected.Size = new System.Drawing.Size(184, 280);
            this.listViewSelected.TabIndex = 16;
            this.listViewSelected.UseCompatibleStateImageBehavior = false;
            this.listViewSelected.View = System.Windows.Forms.View.List;
            this.listViewSelected.DoubleClick += new System.EventHandler(this.listViewSelected_DoubleClick);
            // 
            // listViewSelect
            // 
            this.listViewSelect.LabelWrap = false;
            this.listViewSelect.Location = new System.Drawing.Point(16, 64);
            this.listViewSelect.Name = "listViewSelect";
            this.listViewSelect.Size = new System.Drawing.Size(184, 280);
            this.listViewSelect.TabIndex = 15;
            this.listViewSelect.UseCompatibleStateImageBehavior = false;
            this.listViewSelect.View = System.Windows.Forms.View.List;
            this.listViewSelect.DoubleClick += new System.EventHandler(this.listViewSelect_DoubleClick);
            // 
            // lblSelectedErrorCode
            // 
            this.lblSelectedErrorCode.AutoSize = true;
            this.lblSelectedErrorCode.Location = new System.Drawing.Point(309, 48);
            this.lblSelectedErrorCode.Name = "lblSelectedErrorCode";
            this.lblSelectedErrorCode.Size = new System.Drawing.Size(77, 12);
            this.lblSelectedErrorCode.TabIndex = 14;
            this.lblSelectedErrorCode.Text = "已选不良代码";
            // 
            // lblUnselectedErrorCode
            // 
            this.lblUnselectedErrorCode.AutoSize = true;
            this.lblUnselectedErrorCode.Location = new System.Drawing.Point(13, 48);
            this.lblUnselectedErrorCode.Name = "lblUnselectedErrorCode";
            this.lblUnselectedErrorCode.Size = new System.Drawing.Size(77, 12);
            this.lblUnselectedErrorCode.TabIndex = 13;
            this.lblUnselectedErrorCode.Text = "待选不良代码";
            // 
            // ucButtonAdd
            // 
            this.ucButtonAdd.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonAdd.BackgroundImage")));
            this.ucButtonAdd.ButtonType = UserControl.ButtonTypes.AllRight;
            this.ucButtonAdd.Caption = ">>";
            this.ucButtonAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonAdd.Location = new System.Drawing.Point(213, 130);
            this.ucButtonAdd.Name = "ucButtonAdd";
            this.ucButtonAdd.Size = new System.Drawing.Size(88, 22);
            this.ucButtonAdd.TabIndex = 10;
            this.ucButtonAdd.Click += new System.EventHandler(this.ucButtonAdd_Click);
            // 
            // ucButtonRemove
            // 
            this.ucButtonRemove.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonRemove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonRemove.BackgroundImage")));
            this.ucButtonRemove.ButtonType = UserControl.ButtonTypes.AllLeft;
            this.ucButtonRemove.Caption = "<<";
            this.ucButtonRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ucButtonRemove.Location = new System.Drawing.Point(213, 218);
            this.ucButtonRemove.Name = "ucButtonRemove";
            this.ucButtonRemove.Size = new System.Drawing.Size(88, 22);
            this.ucButtonRemove.TabIndex = 11;
            this.ucButtonRemove.Click += new System.EventHandler(this.ucButtonRemove_Click);
            // 
            // ucLabelComboxErrorGroup
            // 
            this.ucLabelComboxErrorGroup.AllowEditOnlyChecked = true;
            this.ucLabelComboxErrorGroup.Caption = "不良代码组";
            this.ucLabelComboxErrorGroup.Checked = false;
            this.ucLabelComboxErrorGroup.Location = new System.Drawing.Point(17, 8);
            this.ucLabelComboxErrorGroup.Name = "ucLabelComboxErrorGroup";
            this.ucLabelComboxErrorGroup.SelectedIndex = -1;
            this.ucLabelComboxErrorGroup.ShowCheckBox = false;
            this.ucLabelComboxErrorGroup.Size = new System.Drawing.Size(273, 24);
            this.ucLabelComboxErrorGroup.TabIndex = 8;
            this.ucLabelComboxErrorGroup.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelComboxErrorGroup.XAlign = 90;
            this.ucLabelComboxErrorGroup.SelectedIndexChanged += new System.EventHandler(this.ucLabelComboxErrorGroup_SelectedIndexChanged);
            // 
            // FTSErrorEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(514, 454);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxDown);
            this.Controls.Add(this.groupBoxItemInfoForTS);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FTSErrorEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "不良信息编辑";
            this.Load += new System.EventHandler(this.FTSErrorEdit_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FTSErrorEdit_FormClosed);
            this.groupBoxItemInfoForTS.ResumeLayout(false);
            this.groupBoxDown.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        #endregion

        #region 事件

        private void FTSErrorEdit_Load(object sender, EventArgs e)
        {
            this.InitForm();
            //this.InitPageLanguage();
        }

        private void FTSErrorEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DataProvider != null)
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
            }
        }

        private void ucLabelComboxErrorGroup_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.listViewSelect.Clear();

            if (this.ucLabelComboxErrorGroup.SelectedIndex == 0)
            {
                return;
            }

            object[] errorCodes = _TSModelFacade.GetErrorCodeByErrorCodeGroupCode(this.ucLabelComboxErrorGroup.SelectedItemValue.ToString());

            bool found = false;
            foreach (ErrorCodeA errorCode in errorCodes)
            {
                found = false;

                foreach (ListViewItem item in this.listViewSelected.Items)
                {
                    if (((TSErrorCode)item.Tag).ErrorCodeGroup == this.ucLabelComboxErrorGroup.SelectedItemValue.ToString()
                        && ((TSErrorCode)item.Tag).ErrorCode == errorCode.ErrorCode)
                    {
                        found = true;
                        break;
                    }
                }

                if (_DefaultErrorCode != null)
                {
                    if (string.Compare(errorCode.ErrorCode, _DefaultErrorCode.ParameterAlias, true) == 0)
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    ListViewItem newItem = new ListViewItem(errorCode.ErrorDescription + " " + errorCode.ErrorCode);
                    newItem.Tag = errorCode;
                    this.listViewSelect.Items.Add(newItem);
                }
            }
        }

        private void ucButtonSave_Click(object sender, System.EventArgs e)
        {
            if (!_ListHelper.IsDirty)
            {
                return;
            }

            if (_AddTSErrorCodeWhenSave)
            {
                try
                {

                    this.DataProvider.BeginTransaction();

                    _TSFacade.AddTSErrorCode(_CurrentTS, _ListHelper.AddList);
                    _TSFacade.DeleteTSErrorCode(_CurrentTS, _ListHelper.DeleteList);
                    _TSFacade.DeleteTSErrorCode(_CurrentTS.TSId, string.Empty, _DefaultErrorCode.ParameterAlias);

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();

                    this.ShowMessage(ex);

                    return;
                }

                this.ShowMessage(new UserControl.Message(MessageType.Success, "$CS_Save_Success"));
            }
            else
            {
                _SelectedTSErrorCode = _ListHelper.AddList;
            }

            _ListHelper.Clear();

            this.Close();
        }

        private void ucButtonAdd_Click(object sender, System.EventArgs e)
        {
            foreach (ListViewItem addedItem in this.listViewSelect.SelectedItems)
            {
                string errorCodeGroup = this.ucLabelComboxErrorGroup.SelectedItemValue.ToString();
                string errorCode = ((ErrorCodeA)addedItem.Tag).ErrorCode;

                bool found = false;
                foreach (ListViewItem item in this.listViewSelected.Items)
                {
                    if (((TSErrorCode)item.Tag).ErrorCodeGroup == errorCodeGroup
                        && ((TSErrorCode)item.Tag).ErrorCode == errorCode)
                    {
                        found = true;
                        break;
                    }
                }

                TSErrorCode tsErrorCode = _TSFacade.CreateNewTSErrorCode();
                tsErrorCode.TSId = CurrentTS.TSId;
                tsErrorCode.ErrorCodeGroup = errorCodeGroup;                
                tsErrorCode.ErrorCode = errorCode;
                tsErrorCode.MaintainUser = ApplicationService.Current().UserCode;

                ListViewItem newItem = new ListViewItem(string.Format("{0}:{1}", tsErrorCode.ErrorCodeGroup, tsErrorCode.ErrorCode));
                newItem.Tag = tsErrorCode;

                if (!found)
                {
                    this.listViewSelected.Items.Add(newItem);
                    _ListHelper.Add(tsErrorCode);
                }
            }
        }

        private void ucButtonRemove_Click(object sender, System.EventArgs e)
        {
            ArrayList array = new ArrayList();
            foreach (ListViewItem deletedItem in this.listViewSelected.SelectedItems)
            {
                if (_TSFacade.HasInfoBelowTSErrorCode((TSErrorCode)deletedItem.Tag))
                {
                    array.Add(deletedItem.Text);
                }
            }

            if (array.Count > 0)
            {
                string msg = "不良代码组:不良代码已经维护有维修信息，是否确定删除？\n";

                foreach (string code in array)
                {
                    msg += code + "\n";
                }

                if (MessageBox.Show(msg, this.Text, MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            foreach (ListViewItem deletedItem in this.listViewSelected.SelectedItems)
            {
                _ListHelper.Delete((TSErrorCode)deletedItem.Tag);
                deletedItem.Remove();
            }
        }

        private void listViewSelect_DoubleClick(object sender, System.EventArgs e)
        {
            ucButtonAdd_Click(sender, e);
        }

        private void listViewSelected_DoubleClick(object sender, System.EventArgs e)
        {
            ucButtonRemove_Click(sender, e);
        }

        #endregion

        #region 函数

        private void InitForm()
        {
            //设定最上面的Panel和TextBox的标题
            if (string.Compare(_RunningCardTitle, "Item", true) == 0)
            {
                this.groupBoxItemInfoForTS.Text = "产品信息";
                this.ucLabEditRunningCard.Caption = "产品序列号";
            }
            else if (string.Compare(_RunningCardTitle, "Material", true) == 0)
            {
                this.groupBoxItemInfoForTS.Text = "物料信息";
                this.ucLabEditRunningCard.Caption = "物料序列号";
            }

            this.ucLabEditRunningCard.Value = this.CurrentTS.RunningCard;

            try
            {
                this.GetDefaultErrorCode();
                this.SetErrorCodeGroupList(this.CurrentTS.ItemCode, this.CurrentTS.ModelCode);
                this.SetErrorCodeExistList(this.CurrentTS.TSId);
            }
            catch (Exception e)
            {
                this.ShowMessage(new UserControl.Message(e));
            }
        }

        private void GetDefaultErrorCode()
        {
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            object parameter = systemSettingFacade.GetParameter("DEFAULTERRORCODE", "NGCOLLECTDEFAULTERRORCODE");
            if (parameter != null)
            {
                _DefaultErrorCode = parameter as Parameter;
            }
        }

        private void SetErrorCodeExistList(string tsID)
        {
            this.listViewSelected.Clear();

            object[] tsErrorCodes = _TSFacade.GetTSErrorCode(tsID);

            if (tsErrorCodes != null)
            {
                foreach (TSErrorCode tsErrorCode in tsErrorCodes)
                {
                    if (_DefaultErrorCode != null)
                    {
                        if (string.Compare(_DefaultErrorCode.ParameterAlias, tsErrorCode.ErrorCode, true) == 0)
                        {
                            continue;
                        }
                    }
                    ListViewItem item = new ListViewItem(string.Format("{0}:{1}", tsErrorCode.ErrorCodeGroup, tsErrorCode.ErrorCode));
                    item.Tag = tsErrorCode;

                    this.listViewSelected.Items.Add(item);
                }
            }
        }

        private void SetErrorCodeGroupList(string itemCode, string modelCode)
        {
            this.ucLabelComboxErrorGroup.Clear();
            this.ucLabelComboxErrorGroup.AddItem("", "");

            object[] errorCodeGroups = _TSModelFacade.GetErrorCodeGroupByItemCode(itemCode);

            if (errorCodeGroups == null)
            {
                errorCodeGroups = _TSModelFacade.GetErrorCodeGroupByModelCode(modelCode);
            }

            if (errorCodeGroups != null)
            {
                foreach (ErrorCodeGroupA group in errorCodeGroups)
                {
                    this.ucLabelComboxErrorGroup.AddItem(group.ErrorCodeGroupDescription + " " + group.ErrorCodeGroup, group.ErrorCodeGroup);
                }
            }

            this.ucLabelComboxErrorGroup.SelectedIndex = 0;
        }

        #endregion
	}
}
