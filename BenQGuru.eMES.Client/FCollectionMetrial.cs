using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.SMT;

using UserControl;

namespace BenQGuru.eMES.Client
{
    public class FCollectionMetrial : BaseForm
    {
        #region 控件

        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.GroupBox groupBoxCollectObject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblItemCodeDesc;
        private System.Windows.Forms.Label labelItemCodeDesc;
        private System.Windows.Forms.TextBox CollectedCount;

        private UserControl.UCMessage ucMessageInfo;
        private UserControl.UCLabelEdit edtSoftName;
        private UserControl.UCLabelEdit edtECN;
        private UserControl.UCLabelEdit edtSoftInfo;
        private UserControl.UCLabelEdit edtMO;
        private UserControl.UCLabelEdit edtTry;
        private UserControl.UCLabelEdit bRCardLetterULE;
        private UserControl.UCLabelEdit bRCardLenULE;
        private UserControl.UCLabelEdit txtItemCode;
        private UserControl.UCButton btnExit;

        public UserControl.UCLabelEdit edtInput;
        public UserControl.UCLabelEdit edtINNO;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet opsetCollectObject;
        #endregion

        #region 全局变量

        private const string opCollectAutoCollectLotPart = "0";
        private const string opCollectNeedInputLotPart = "1";
        private string _FunctionName = string.Empty;
        private string _SourceRCard = string.Empty;//最初的(源)序列号 Add By Bernard @ 2010-11-02

        private Hashtable listOpBomKeyParts = new Hashtable();
        private ProductInfo productInfo = null;
        private Hashtable listActionCheckStatus = new Hashtable();
        private Resource Resource;

        private MaterialFacade _MaterialFacade = null;
        private DataCollectFacade _DataCollectFacade = null;

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        #region 流程控制

        private int _FlowControl = -1;

        private bool _NeedInputLotPart = false;
        private int _CollectedCount = 0;

        private bool _CancelChanged = false;
        private bool _IgnoreChangedEvent = false;

        private int FlowControl
        {
            get
            {
                return _FlowControl;
            }
            set
            {
                _FlowControl = value;
                //初始化
                if (_FlowControl == -1)
                {
                    //opsetCollectObject.Value = opCollectAutoCollectLotPart;//add by hiro 
                    _inno = string.Empty;
                    opsetCollectObject.Enabled = true;
                    edtINNO.Enabled = true;
                    edtECN.Enabled = true;
                    edtMO.Enabled = true;
                    edtSoftInfo.Enabled = true;
                    edtSoftName.Enabled = true;
                    edtTry.Enabled = true;
                    Collect();

                }
                //ID采集后，不允许再修改附属信息
                if (FlowControl > 0)
                {
                    opsetCollectObject.Enabled = true;
                    opsetCollectObject.BackColor = Color.FromArgb(220, 220, 220);
                    opsetCollectObject.ItemAppearance.BackColor = Color.FromArgb(220, 220, 220);
                    //Laws Lu,2005/09/10,将来需要新增	方便连续采集
                    //					if(opsetCollectObject.CheckedItem.DataValue.ToString() != opCollectAutoCollectLotPart)
                    //					{
                    edtINNO.Enabled = false;
                    edtMO.InnerTextBox.Enabled = false;
                    //					}
                    edtECN.Enabled = false;
                    edtSoftInfo.Enabled = false;
                    edtSoftName.Enabled = false;
                    edtTry.Enabled = false;
                }
            }
        }

        #endregion

        #region 中间数据

        private string _inno;
        private string INNO
        {
            get
            {
                if (edtINNO.Checked)
                {
                    return edtINNO.Value.Trim();
                }
                else
                    return _inno;
            }
            set
            {
                _inno = value.Trim();
            }
        }

        //private string ID;
        private int _CollectIDCount;
        private bool _HaveCollectID1 = false;
        private bool _HaveCollectID2 = false;
        private bool _HaveCollectID3 = false;
        private bool _HaveCollectMaterial = false;

        private IDInfo _IDInfo = new IDInfo();

        private object[] _OPBOMDetailList;
        private ArrayList _OPBOMDetailArrayList = new ArrayList();

        private object[] _OPBOMDetailKeyPartList;
        private ArrayList _OPBOMDetailKeyPartArrayList = new ArrayList();

        private object[] _PreparedLotPartList;
        private ArrayList _PreparedLotPartArrayList = new ArrayList();

        private object[] _objBomDetailNotFilter;
        private CheckBox checkMACID2;
        private CheckBox checkMACID3;
        private CheckBox checkMACID1;
        private CheckBox ckbSame;
        private UCButton btnDocuments;


        ArrayList _InputPartList = new ArrayList();

        #endregion

        #endregion

        #region Form初始化自动生成

        public FCollectionMetrial()
        {
            InitializeComponent();

            FlowControl = -1;

            //抓取并设定AutoMaterialOP
            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
            Operation2Resource op2Res = (Operation2Resource)baseModelFacade.GetOperationByResource(ApplicationService.Current().ResourceCode);
            if (op2Res != null)
            {
                string opCode = op2Res.OPCode.Trim().ToUpper();
                Domain.BaseSetting.Parameter autoMaterialOP = (Domain.BaseSetting.Parameter)(new SystemSettingFacade(this.DataProvider)).GetParameter(opCode, "AUTOMATERIAL");
                this.ckbSame.Checked = (autoMaterialOP != null && autoMaterialOP.ParameterAlias.Trim().ToUpper() == opCode);
            }

            UserControl.UIStyleBuilder.FormUI(this);
            opsetCollectObject.Appearance.BackColorDisabled = Color.FromArgb(220, 220, 220);
            edtSoftInfo_CheckBoxCheckedChanged(null, null);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCollectionMetrial));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.ckbSame = new System.Windows.Forms.CheckBox();
            this.checkMACID2 = new System.Windows.Forms.CheckBox();
            this.checkMACID3 = new System.Windows.Forms.CheckBox();
            this.checkMACID1 = new System.Windows.Forms.CheckBox();
            this.CollectedCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.edtInput = new UserControl.UCLabelEdit();
            this.btnExit = new UserControl.UCButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxCollectObject = new System.Windows.Forms.GroupBox();
            this.opsetCollectObject = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.bRCardLetterULE = new UserControl.UCLabelEdit();
            this.bRCardLenULE = new UserControl.UCLabelEdit();
            this.edtSoftName = new UserControl.UCLabelEdit();
            this.edtECN = new UserControl.UCLabelEdit();
            this.edtSoftInfo = new UserControl.UCLabelEdit();
            this.edtMO = new UserControl.UCLabelEdit();
            this.edtTry = new UserControl.UCLabelEdit();
            this.edtINNO = new UserControl.UCLabelEdit();
            this.ucMessageInfo = new UserControl.UCMessage();
            this.lblItemCodeDesc = new System.Windows.Forms.Label();
            this.labelItemCodeDesc = new System.Windows.Forms.Label();
            this.txtItemCode = new UserControl.UCLabelEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDocuments = new UserControl.UCButton();
            this.groupBoxInput.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxCollectObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opsetCollectObject)).BeginInit();
            this.groupBoxInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInput.Controls.Add(this.ckbSame);
            this.groupBoxInput.Controls.Add(this.checkMACID2);
            this.groupBoxInput.Controls.Add(this.checkMACID3);
            this.groupBoxInput.Controls.Add(this.checkMACID1);
            this.groupBoxInput.Controls.Add(this.CollectedCount);
            this.groupBoxInput.Controls.Add(this.label1);
            this.groupBoxInput.Controls.Add(this.edtInput);
            this.groupBoxInput.Controls.Add(this.btnExit);
            this.groupBoxInput.Location = new System.Drawing.Point(0, 536);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(924, 48);
            this.groupBoxInput.TabIndex = 153;
            this.groupBoxInput.TabStop = false;
            // 
            // ckbSame
            // 
            this.ckbSame.Enabled = false;
            this.ckbSame.Location = new System.Drawing.Point(526, 16);
            this.ckbSame.Name = "ckbSame";
            this.ckbSame.Size = new System.Drawing.Size(182, 20);
            this.ckbSame.TabIndex = 232;
            this.ckbSame.Text = "管控料和产品的序列号相同";
            // 
            // checkMACID2
            // 
            this.checkMACID2.Location = new System.Drawing.Point(363, 16);
            this.checkMACID2.Name = "checkMACID2";
            this.checkMACID2.Size = new System.Drawing.Size(70, 20);
            this.checkMACID2.TabIndex = 231;
            this.checkMACID2.Text = "采集ID2";
            this.checkMACID2.CheckedChanged += new System.EventHandler(this.checkMACID2_CheckedChanged);
            // 
            // checkMACID3
            // 
            this.checkMACID3.Location = new System.Drawing.Point(441, 16);
            this.checkMACID3.Name = "checkMACID3";
            this.checkMACID3.Size = new System.Drawing.Size(78, 20);
            this.checkMACID3.TabIndex = 230;
            this.checkMACID3.Text = "采集ID3";
            this.checkMACID3.CheckedChanged += new System.EventHandler(this.checkMACID3_CheckedChanged);
            // 
            // checkMACID1
            // 
            this.checkMACID1.Location = new System.Drawing.Point(281, 16);
            this.checkMACID1.Name = "checkMACID1";
            this.checkMACID1.Size = new System.Drawing.Size(74, 20);
            this.checkMACID1.TabIndex = 229;
            this.checkMACID1.Text = "采集ID1";
            this.checkMACID1.CheckedChanged += new System.EventHandler(this.checkMACID1_CheckedChanged);
            // 
            // CollectedCount
            // 
            this.CollectedCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CollectedCount.Enabled = false;
            this.CollectedCount.Location = new System.Drawing.Point(863, 16);
            this.CollectedCount.Name = "CollectedCount";
            this.CollectedCount.Size = new System.Drawing.Size(49, 21);
            this.CollectedCount.TabIndex = 12;
            this.CollectedCount.Text = "0";
            this.CollectedCount.Visible = false;
            this.CollectedCount.Leave += new System.EventHandler(this.CollectedCount_Leave);
            this.CollectedCount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CollectedCount_KeyUp);
            this.CollectedCount.Enter += new System.EventHandler(this.CollectedCount_Enter);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(793, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "已采集数量";
            this.label1.Visible = false;
            // 
            // edtInput
            // 
            this.edtInput.AllowEditOnlyChecked = false;
            this.edtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.edtInput.AutoSelectAll = false;
            this.edtInput.AutoUpper = true;
            this.edtInput.Caption = "输入框";
            this.edtInput.Checked = false;
            this.edtInput.EditType = UserControl.EditTypes.String;
            this.edtInput.Location = new System.Drawing.Point(17, 16);
            this.edtInput.MaxLength = 40;
            this.edtInput.Multiline = false;
            this.edtInput.Name = "edtInput";
            this.edtInput.PasswordChar = '\0';
            this.edtInput.ReadOnly = false;
            this.edtInput.ShowCheckBox = false;
            this.edtInput.Size = new System.Drawing.Size(249, 24);
            this.edtInput.TabIndex = 9;
            this.edtInput.TabNext = false;
            this.edtInput.Value = "";
            this.edtInput.WidthType = UserControl.WidthTypes.Long;
            this.edtInput.XAlign = 66;
            this.edtInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtInput_TxtboxKeyPress);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(715, 16);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.groupBoxCollectObject);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(937, 35);
            this.panel1.TabIndex = 155;
            // 
            // groupBoxCollectObject
            // 
            this.groupBoxCollectObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCollectObject.Controls.Add(this.opsetCollectObject);
            this.groupBoxCollectObject.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxCollectObject.Location = new System.Drawing.Point(0, 12);
            this.groupBoxCollectObject.Name = "groupBoxCollectObject";
            this.groupBoxCollectObject.Size = new System.Drawing.Size(922, 159);
            this.groupBoxCollectObject.TabIndex = 2;
            this.groupBoxCollectObject.TabStop = false;
            this.groupBoxCollectObject.Text = "采集方式";
            // 
            // opsetCollectObject
            // 
            appearance1.FontData.BoldAsString = "False";
            this.opsetCollectObject.Appearance = appearance1;
            this.opsetCollectObject.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            valueListItem1.DataValue = "0";
            valueListItem1.DisplayText = "批控管料+KeyParts采集";
            valueListItem2.DataValue = "1";
            valueListItem2.DisplayText = "混合采集";
            this.opsetCollectObject.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2});
            this.opsetCollectObject.Location = new System.Drawing.Point(16, 16);
            this.opsetCollectObject.Name = "opsetCollectObject";
            this.opsetCollectObject.Size = new System.Drawing.Size(432, 16);
            this.opsetCollectObject.TabIndex = 1;
            this.opsetCollectObject.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.opsetCollectObject.Visible = false;
            this.opsetCollectObject.ValueChanged += new System.EventHandler(this.opsetCollectObject_ValueChanged);
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInfo.Controls.Add(this.bRCardLetterULE);
            this.groupBoxInfo.Controls.Add(this.bRCardLenULE);
            this.groupBoxInfo.Controls.Add(this.edtSoftName);
            this.groupBoxInfo.Controls.Add(this.edtECN);
            this.groupBoxInfo.Controls.Add(this.edtSoftInfo);
            this.groupBoxInfo.Controls.Add(this.edtMO);
            this.groupBoxInfo.Controls.Add(this.edtTry);
            this.groupBoxInfo.Controls.Add(this.edtINNO);
            this.groupBoxInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxInfo.Location = new System.Drawing.Point(-1, 6);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(924, 131);
            this.groupBoxInfo.TabIndex = 3;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "附属信息指定";
            // 
            // bRCardLetterULE
            // 
            this.bRCardLetterULE.AllowEditOnlyChecked = true;
            this.bRCardLetterULE.AutoSelectAll = false;
            this.bRCardLetterULE.AutoUpper = true;
            this.bRCardLetterULE.Caption = "产品序列号首字符";
            this.bRCardLetterULE.Checked = false;
            this.bRCardLetterULE.EditType = UserControl.EditTypes.String;
            this.bRCardLetterULE.Enabled = false;
            this.bRCardLetterULE.Location = new System.Drawing.Point(513, 16);
            this.bRCardLetterULE.MaxLength = 40;
            this.bRCardLetterULE.Multiline = false;
            this.bRCardLetterULE.Name = "bRCardLetterULE";
            this.bRCardLetterULE.PasswordChar = '\0';
            this.bRCardLetterULE.ReadOnly = false;
            this.bRCardLetterULE.ShowCheckBox = true;
            this.bRCardLetterULE.Size = new System.Drawing.Size(258, 24);
            this.bRCardLetterULE.TabIndex = 26;
            this.bRCardLetterULE.TabNext = false;
            this.bRCardLetterULE.Value = "";
            this.bRCardLetterULE.WidthType = UserControl.WidthTypes.Normal;
            this.bRCardLetterULE.XAlign = 638;
            // 
            // bRCardLenULE
            // 
            this.bRCardLenULE.AllowEditOnlyChecked = true;
            this.bRCardLenULE.AutoSelectAll = false;
            this.bRCardLenULE.AutoUpper = true;
            this.bRCardLenULE.Caption = "产品序列号长度";
            this.bRCardLenULE.Checked = false;
            this.bRCardLenULE.EditType = UserControl.EditTypes.Integer;
            this.bRCardLenULE.Enabled = false;
            this.bRCardLenULE.Location = new System.Drawing.Point(257, 16);
            this.bRCardLenULE.MaxLength = 40;
            this.bRCardLenULE.Multiline = false;
            this.bRCardLenULE.Name = "bRCardLenULE";
            this.bRCardLenULE.PasswordChar = '\0';
            this.bRCardLenULE.ReadOnly = false;
            this.bRCardLenULE.ShowCheckBox = true;
            this.bRCardLenULE.Size = new System.Drawing.Size(246, 24);
            this.bRCardLenULE.TabIndex = 25;
            this.bRCardLenULE.TabNext = false;
            this.bRCardLenULE.Value = "";
            this.bRCardLenULE.WidthType = UserControl.WidthTypes.Normal;
            this.bRCardLenULE.XAlign = 370;
            // 
            // edtSoftName
            // 
            this.edtSoftName.AllowEditOnlyChecked = true;
            this.edtSoftName.AutoSelectAll = false;
            this.edtSoftName.AutoUpper = true;
            this.edtSoftName.Caption = "采集软件名称";
            this.edtSoftName.Checked = false;
            this.edtSoftName.EditType = UserControl.EditTypes.String;
            this.edtSoftName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edtSoftName.Location = new System.Drawing.Point(17, 67);
            this.edtSoftName.MaxLength = 40;
            this.edtSoftName.Multiline = false;
            this.edtSoftName.Name = "edtSoftName";
            this.edtSoftName.PasswordChar = '\0';
            this.edtSoftName.ReadOnly = false;
            this.edtSoftName.ShowCheckBox = true;
            this.edtSoftName.Size = new System.Drawing.Size(234, 24);
            this.edtSoftName.TabIndex = 7;
            this.edtSoftName.TabNext = true;
            this.edtSoftName.Value = "";
            this.edtSoftName.WidthType = UserControl.WidthTypes.Normal;
            this.edtSoftName.XAlign = 118;
            this.edtSoftName.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.INNO_TxtboxKeyPress);
            // 
            // edtECN
            // 
            this.edtECN.AllowEditOnlyChecked = true;
            this.edtECN.AutoSelectAll = false;
            this.edtECN.AutoUpper = true;
            this.edtECN.Caption = "采集ECN号码   ";
            this.edtECN.Checked = false;
            this.edtECN.EditType = UserControl.EditTypes.String;
            this.edtECN.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edtECN.Location = new System.Drawing.Point(257, 43);
            this.edtECN.MaxLength = 40;
            this.edtECN.Multiline = false;
            this.edtECN.Name = "edtECN";
            this.edtECN.PasswordChar = '\0';
            this.edtECN.ReadOnly = false;
            this.edtECN.ShowCheckBox = true;
            this.edtECN.Size = new System.Drawing.Size(246, 24);
            this.edtECN.TabIndex = 6;
            this.edtECN.TabNext = true;
            this.edtECN.Value = "";
            this.edtECN.WidthType = UserControl.WidthTypes.Normal;
            this.edtECN.XAlign = 370;
            this.edtECN.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.INNO_TxtboxKeyPress);
            // 
            // edtSoftInfo
            // 
            this.edtSoftInfo.AllowEditOnlyChecked = true;
            this.edtSoftInfo.AutoSelectAll = false;
            this.edtSoftInfo.AutoUpper = true;
            this.edtSoftInfo.Caption = "采集软件版本";
            this.edtSoftInfo.Checked = false;
            this.edtSoftInfo.EditType = UserControl.EditTypes.String;
            this.edtSoftInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edtSoftInfo.Location = new System.Drawing.Point(17, 43);
            this.edtSoftInfo.MaxLength = 40;
            this.edtSoftInfo.Multiline = false;
            this.edtSoftInfo.Name = "edtSoftInfo";
            this.edtSoftInfo.PasswordChar = '\0';
            this.edtSoftInfo.ReadOnly = false;
            this.edtSoftInfo.ShowCheckBox = true;
            this.edtSoftInfo.Size = new System.Drawing.Size(234, 24);
            this.edtSoftInfo.TabIndex = 5;
            this.edtSoftInfo.TabNext = true;
            this.edtSoftInfo.Value = "";
            this.edtSoftInfo.WidthType = UserControl.WidthTypes.Normal;
            this.edtSoftInfo.XAlign = 118;
            this.edtSoftInfo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.INNO_TxtboxKeyPress);
            this.edtSoftInfo.CheckBoxCheckedChanged += new System.EventHandler(this.edtSoftInfo_CheckBoxCheckedChanged);
            // 
            // edtMO
            // 
            this.edtMO.AllowEditOnlyChecked = true;
            this.edtMO.AutoSelectAll = false;
            this.edtMO.AutoUpper = true;
            this.edtMO.Caption = "设定归属工单";
            this.edtMO.Checked = false;
            this.edtMO.EditType = UserControl.EditTypes.String;
            this.edtMO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edtMO.Location = new System.Drawing.Point(17, 16);
            this.edtMO.MaxLength = 40;
            this.edtMO.Multiline = false;
            this.edtMO.Name = "edtMO";
            this.edtMO.PasswordChar = '\0';
            this.edtMO.ReadOnly = false;
            this.edtMO.ShowCheckBox = true;
            this.edtMO.Size = new System.Drawing.Size(234, 24);
            this.edtMO.TabIndex = 3;
            this.edtMO.TabNext = true;
            this.edtMO.Value = "";
            this.edtMO.WidthType = UserControl.WidthTypes.Normal;
            this.edtMO.XAlign = 118;
            this.edtMO.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.INNO_TxtboxKeyPress);
            this.edtMO.CheckBoxCheckedChanged += new System.EventHandler(this.edtMO_CheckBoxCheckedChanged);
            // 
            // edtTry
            // 
            this.edtTry.AllowEditOnlyChecked = true;
            this.edtTry.AutoSelectAll = false;
            this.edtTry.AutoUpper = true;
            this.edtTry.Caption = "采集试流单ID    ";
            this.edtTry.Checked = false;
            this.edtTry.EditType = UserControl.EditTypes.String;
            this.edtTry.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edtTry.Location = new System.Drawing.Point(513, 43);
            this.edtTry.MaxLength = 0;
            this.edtTry.Multiline = false;
            this.edtTry.Name = "edtTry";
            this.edtTry.PasswordChar = '\0';
            this.edtTry.ReadOnly = false;
            this.edtTry.ShowCheckBox = true;
            this.edtTry.Size = new System.Drawing.Size(258, 24);
            this.edtTry.TabIndex = 4;
            this.edtTry.TabNext = true;
            this.edtTry.Value = "";
            this.edtTry.WidthType = UserControl.WidthTypes.Normal;
            this.edtTry.XAlign = 638;
            this.edtTry.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtTry_TxtboxKeyPress);
            this.edtTry.CheckBoxCheckedChanged += new System.EventHandler(this.edtTry_CheckBoxCheckedChanged);
            // 
            // edtINNO
            // 
            this.edtINNO.AllowEditOnlyChecked = true;
            this.edtINNO.AutoSelectAll = false;
            this.edtINNO.AutoUpper = true;
            this.edtINNO.Caption = "当前集成上料号";
            this.edtINNO.Checked = false;
            this.edtINNO.EditType = UserControl.EditTypes.String;
            this.edtINNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edtINNO.Location = new System.Drawing.Point(257, 67);
            this.edtINNO.MaxLength = 40;
            this.edtINNO.Multiline = false;
            this.edtINNO.Name = "edtINNO";
            this.edtINNO.PasswordChar = '\0';
            this.edtINNO.ReadOnly = false;
            this.edtINNO.ShowCheckBox = true;
            this.edtINNO.Size = new System.Drawing.Size(246, 24);
            this.edtINNO.TabIndex = 2;
            this.edtINNO.TabNext = true;
            this.edtINNO.Value = "";
            this.edtINNO.Visible = false;
            this.edtINNO.WidthType = UserControl.WidthTypes.Normal;
            this.edtINNO.XAlign = 370;
            this.edtINNO.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.INNO_TxtboxKeyPress);
            // 
            // ucMessageInfo
            // 
            this.ucMessageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMessageInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessageInfo.ButtonVisible = false;
            this.ucMessageInfo.Location = new System.Drawing.Point(0, 139);
            this.ucMessageInfo.Name = "ucMessageInfo";
            this.ucMessageInfo.Size = new System.Drawing.Size(924, 397);
            this.ucMessageInfo.TabIndex = 156;
            this.ucMessageInfo.TabStop = false;
            this.ucMessageInfo.WorkingErrorAdded += new UserControl.WorkingErrorAddedEventHandler(this.ucMessageInfo_WorkingErrorAdded);
            // 
            // lblItemCodeDesc
            // 
            this.lblItemCodeDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCodeDesc.Location = new System.Drawing.Point(374, 12);
            this.lblItemCodeDesc.Name = "lblItemCodeDesc";
            this.lblItemCodeDesc.Size = new System.Drawing.Size(74, 24);
            this.lblItemCodeDesc.TabIndex = 215;
            this.lblItemCodeDesc.Text = "产品描述";
            this.lblItemCodeDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelItemCodeDesc
            // 
            this.labelItemCodeDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemCodeDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemCodeDesc.Location = new System.Drawing.Point(454, 10);
            this.labelItemCodeDesc.Name = "labelItemCodeDesc";
            this.labelItemCodeDesc.Size = new System.Drawing.Size(425, 26);
            this.labelItemCodeDesc.TabIndex = 214;
            this.labelItemCodeDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtItemCode
            // 
            this.txtItemCode.AllowEditOnlyChecked = true;
            this.txtItemCode.AutoSelectAll = false;
            this.txtItemCode.AutoUpper = true;
            this.txtItemCode.BackColor = System.Drawing.Color.Transparent;
            this.txtItemCode.Caption = "产品";
            this.txtItemCode.Checked = false;
            this.txtItemCode.EditType = UserControl.EditTypes.String;
            this.txtItemCode.Enabled = false;
            this.txtItemCode.Location = new System.Drawing.Point(81, 14);
            this.txtItemCode.MaxLength = 40;
            this.txtItemCode.Multiline = false;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.PasswordChar = '\0';
            this.txtItemCode.ReadOnly = false;
            this.txtItemCode.ShowCheckBox = false;
            this.txtItemCode.Size = new System.Drawing.Size(170, 24);
            this.txtItemCode.TabIndex = 216;
            this.txtItemCode.TabNext = true;
            this.txtItemCode.TabStop = false;
            this.txtItemCode.Value = "";
            this.txtItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtItemCode.XAlign = 118;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnDocuments);
            this.groupBox1.Controls.Add(this.txtItemCode);
            this.groupBox1.Controls.Add(this.lblItemCodeDesc);
            this.groupBox1.Controls.Add(this.labelItemCodeDesc);
            this.groupBox1.Location = new System.Drawing.Point(-1, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(924, 44);
            this.groupBox1.TabIndex = 217;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "产品信息";
            // 
            // btnDocuments
            // 
            this.btnDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDocuments.BackColor = System.Drawing.SystemColors.Control;
            this.btnDocuments.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDocuments.BackgroundImage")));
            this.btnDocuments.ButtonType = UserControl.ButtonTypes.None;
            this.btnDocuments.Caption = "文档";
            this.btnDocuments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDocuments.Location = new System.Drawing.Point(267, 14);
            this.btnDocuments.Name = "btnDocuments";
            this.btnDocuments.Size = new System.Drawing.Size(88, 22);
            this.btnDocuments.TabIndex = 217;
            this.btnDocuments.Click += new System.EventHandler(this.btnDocuments_Click);
            // 
            // FCollectionMetrial
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(924, 584);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.ucMessageInfo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxInput);
            this.Name = "FCollectionMetrial";
            this.Text = "上料采集";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Deactivate += new System.EventHandler(this.FCollectionMetrial_Deactivate);
            this.Load += new System.EventHandler(this.FCollectionMetrial_Load);
            this.Activated += new System.EventHandler(this.FCollectionMetrial_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FCollectionMetrial_FormClosed);
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBoxCollectObject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.opsetCollectObject)).EndInit();
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        #region 事件

        private void FCollectionMetrial_Activated(object sender, System.EventArgs e)
        {
            if (opsetCollectObject.Value == null)
            {
                opsetCollectObject.Value = opCollectAutoCollectLotPart;
            }

            ApplicationRun.GetQtyForm().Show();
        }

        private void FCollectionMetrial_Deactivate(object sender, System.EventArgs e)
        {
            //edtINNO.Checked = false;
            ApplicationRun.GetQtyForm().Hide();
        }

        private void FCollectionMetrial_Load(object sender, System.EventArgs e)
        {
            this._FunctionName = this.Text;

            ApplicationRun.GetQtyForm().Show();

            //Point pt = new Point(945, 260);
            ApplicationRun.GetQtyForm().StartPosition = FormStartPosition.Manual;
            //ApplicationRun.GetQtyForm().Location = pt;

            _MaterialFacade = new MaterialFacade(this.DataProvider);
            _DataCollectFacade = new DataCollectFacade(this.DataProvider);

            //this.InitPageLanguage();
        }

        private void FCollectionMetrial_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DataProvider != null)
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();

            ApplicationRun.GetQtyForm().Hide();
        }

        private void CollectedCount_Enter(object sender, System.EventArgs e)
        {
            CollectedCount.BackColor = Color.GreenYellow;
        }

        private void CollectedCount_Leave(object sender, System.EventArgs e)
        {
            CollectedCount.BackColor = Color.White;
        }

        private void CollectedCount_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //			if(!IsNumber(e.KeyChar))
            //			{
            //				CollectedCount.Text = "0";
            //			}
        }

        private void CollectedCount_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData != Keys.D0 && e.KeyData != Keys.D1 && e.KeyData != Keys.D2 && e.KeyData != Keys.D3 &&
                e.KeyData != Keys.D4 && e.KeyData != Keys.D5 && e.KeyData != Keys.D6 && e.KeyData != Keys.D7 &&
                e.KeyData != Keys.D8 && e.KeyData != Keys.D9)
            {
                CollectedCount.Text = "0";
            }
        }

        private void edtTry_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.edtTry_TxtboxKeyPress();
            }
        }

        private void edtTry_TxtboxKeyPress()
        {
            if (this.edtTry.Checked == true && string.IsNullOrEmpty(this.edtTry.Value.Trim()))
            {
                ucMessageInfo.AddEx(this._FunctionName, "$CS_TryLotCode: " + this.edtTry.Value, new UserControl.Message(MessageType.Normal, "$CS_PleasePressInputTry"), true);
            }
            else
            {
                TryFacade tryfacade = new TryFacade(this.DataProvider);
                object objTry = tryfacade.GetTry(this.edtTry.Value.Trim().ToUpper());
                if (objTry != null)
                {
                    if (((Try)objTry).Status != TryStatus.STATUS_PRODUCE && ((Try)objTry).Status != TryStatus.STATUS_RELEASE)
                    {
                        ucMessageInfo.AddEx(this._FunctionName, "$CS_TryLotCode: " + this.edtTry.Value, new UserControl.Message(MessageType.Error, "$Error_CS_Try_Should_be_Release_or_Open"), true);
                        this.edtTry.InnerTextBox.Enabled = false;
                        return;
                    }
                }
            }
            this.edtTry.InnerTextBox.Enabled = false;
            ucMessageInfo.AddEx(">>$CS_PleaseInputID");
            this.edtInput.TextFocus(true, true);
        }

        private void edtTry_CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            if (this.edtTry.Checked)
            {
                edtTry.Enabled = true;

                edtTry.InnerTextBox.ReadOnly = true;
                FTryLotNo fTryLotNo = new FTryLotNo();
                fTryLotNo.Owner = this;
                fTryLotNo.TrySelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(softVerSelector_TrySelectedEvent);
                fTryLotNo.ShowDialog();
                fTryLotNo = null;

                if (string.IsNullOrEmpty(edtTry.Value.Trim()))
                {
                    edtTry.Checked = false;
                }
                if (this.edtTry.Checked)
                {
                    this.edtTry_TxtboxKeyPress();
                }
            }
        }

        private void edtINNO_CheckBoxCheckedChanged(object sender, System.EventArgs e)
        {


        }

        private void edtINNO_Leave(object sender, System.EventArgs e)
        {
            //karron qiu,2005-10-20
            //请依照之前的上料采集Spec修改，或者取消这个功能，
            //也就是如果上料采集需要集成上料号信息必须勾选集成上料号项，且必须输入
            //			if(opsetCollectObject.Value.ToString() !=opCollectKeyParts && edtINNO.Value.Trim() == string.Empty)
            //			{
            //				edtINNO.TextFocus(false, true);
            //				edtINNO.TextFocus(false, true);
            //			}
        }

        private void INNO_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                ClearVariables();

                if (this.edtMO.Checked)
                {
                    MOFacade moFacade = new MOFacade(this._domainDataProvider);
                    object objmo = moFacade.GetMO(this.edtMO.Value.Trim().ToString());
                    if (objmo != null)
                    {
                        this.txtItemCode.Value = ((MO)objmo).ItemCode.Trim().ToString();

                        ItemFacade itemFacede = new ItemFacade(this._domainDataProvider);
                        object objitem = itemFacede.GetItem(((MO)objmo).ItemCode.Trim().ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID);

                        if (objitem == null)
                        {
                            ucMessageInfo.AddEx(this._FunctionName, "$CS_MO: " + this.edtMO.Value, new UserControl.Message(MessageType.Error, "$Error_ItemCode_NotExist $Domain_ItemCode=" + ((MO)objmo).ItemCode.Trim().ToString()), true);
                            this.edtMO.TextFocus(false, true);
                            return;
                        }

                        Item item = objitem as Item;
                        this.labelItemCodeDesc.Text = item.ItemDescription;
                        object item2sncheck = itemFacede.GetItem2SNCheck(item.ItemCode, ItemCheckType.ItemCheckType_SERIAL);
                        if (item2sncheck == null)
                        {
                            // Item2SNCheck not exist
                            ucMessageInfo.AddEx(this._FunctionName, "$CS_MO: " + this.edtMO.Value, new UserControl.Message(MessageType.Error, "$Error_NoItemSNCheckInfo $Domain_ItemCode=" + item.ItemCode), true);
                            this.bRCardLetterULE.Value = "";
                            this.bRCardLetterULE.Checked = false;
                            this.bRCardLenULE.Checked = false;
                            this.bRCardLenULE.Value = "";
                            this.edtMO.TextFocus(false, true);
                            return;
                        }

                        Item2SNCheck item2SNCheck = item2sncheck as Item2SNCheck;
                        SystemSettingFacade ssf = new SystemSettingFacade(this.DataProvider);
                        object para = ssf.GetParameter("PRODUCTCODECONTROLSTATUS", "PRODUCTCODECONTROLSTATUS");
                        if (item2SNCheck.SNPrefix.Length != 0)
                        {
                            this.bRCardLetterULE.Checked = true;
                            this.bRCardLetterULE.Value = item2SNCheck.SNPrefix;
                            if (para != null)
                            {
                                if (string.Compare(((Parameter)para).ParameterAlias, "1", true) == 0)
                                {
                                    this.bRCardLetterULE.Enabled = false;
                                }
                                else
                                {
                                    this.bRCardLetterULE.Enabled = true;
                                }
                            }
                            else
                            {
                                this.bRCardLetterULE.Enabled = true;
                            }
                        }
                        else
                        {
                            this.bRCardLetterULE.Enabled = true;
                        }

                        if (item2SNCheck.SNLength != 0)
                        {
                            this.bRCardLenULE.Checked = true;
                            this.bRCardLenULE.Value = item2SNCheck.SNLength.ToString();
                            if (para != null)
                            {
                                if (string.Compare(((Parameter)para).ParameterAlias, "1", true) == 0)
                                {
                                    this.bRCardLenULE.Enabled = false;
                                }
                                else
                                {
                                    this.bRCardLenULE.Enabled = true;
                                }
                            }
                            else
                            {
                                this.bRCardLenULE.Enabled = true;
                            }
                        }
                        else
                        {
                            this.bRCardLenULE.Enabled = true;
                        }
                    }
                    else
                    {
                        ucMessageInfo.AddEx(this._FunctionName, "$CS_MO: " + this.edtMO.Value, new UserControl.Message(MessageType.Error, "$CS_MO_NOT_EXIST"), true);
                        this.edtMO.TextFocus(false, true);
                        return;
                    }

                    this.edtMO.InnerTextBox.Enabled = false;
                    this.edtInput.TextFocus(false, true);
                }
            }
        }

        private void edtMO_CheckBoxCheckedChanged(object sender, System.EventArgs e)
        {
            if (edtMO.Checked == false)
            {
                this.bRCardLenULE.Value = String.Empty;
                this.bRCardLetterULE.Value = String.Empty;
                this.labelItemCodeDesc.Text = string.Empty;
                this.txtItemCode.Value = string.Empty;

                this.edtSoftInfo.Value = string.Empty;
                this.edtECN.Value = string.Empty;
                this.edtTry.Value = string.Empty;
                this.edtSoftName.Value = string.Empty;

                this.edtSoftInfo.Checked = false;
                this.edtECN.Checked = false;
                this.edtTry.Checked = false;
                this.edtSoftName.Checked = false;

                this.bRCardLenULE.Checked = false;
                this.bRCardLetterULE.Checked = false;
                this.bRCardLenULE.Enabled = false;
                this.bRCardLetterULE.Enabled = false;
            }
            if (edtMO.Checked == true)
            {
                this.bRCardLenULE.Enabled = true;
                this.bRCardLetterULE.Enabled = true;
            }
        }

        private void opsetCollectObject_ValueChanged(object sender, System.EventArgs e)
        {

            if (_IgnoreChangedEvent)
            {
                _IgnoreChangedEvent = false;
                return;
            }

            //采集方式改变
            if (_OPBOMDetailArrayList.Count > 0)
            {
                if (_CancelChanged)
                {
                    _CancelChanged = false;
                    _IgnoreChangedEvent = true;
                    opsetCollectObject.CheckedIndex = 1 - opsetCollectObject.CheckedIndex;
                }
                else if (MessageBox.Show(MutiLanguages.ParserMessage("$Change_Collected_Method"), MutiLanguages.ParserMessage("$ShowMessage"), MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {

                    _CancelChanged = false;
                    _IgnoreChangedEvent = false;

                    ClearVariables();
                }
                else
                {
                    _CancelChanged = true;
                    _IgnoreChangedEvent = true;
                    opsetCollectObject.CheckedIndex = 1 - opsetCollectObject.CheckedIndex;
                }
            }

            edtInput.TextFocus(true, true);

        }

        private void ucMessageInfo_WorkingErrorAdded(object sender, WorkingErrorAddedEventArgs e)
        {
            CSHelper.ucMessageWorkingErrorAdded(e, this.DataProvider);
        }

        private void edtSoftInfo_CheckBoxCheckedChanged(object sender, System.EventArgs e)
        {
            if (edtSoftInfo.Checked)
            {
                edtSoftName.Enabled = true;

                // Added by HI1/Venus.Feng on 20081107 for hisense Version : Add Software Version Selector
                edtSoftInfo.InnerTextBox.ReadOnly = true;
                FSoftVersionSelector softVerSelector = new FSoftVersionSelector();
                softVerSelector.Owner = this;
                softVerSelector.SoftVersionSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(softVerSelector_SoftVersionSelectedEvent);
                softVerSelector.ShowDialog();
                softVerSelector = null;

                if (string.IsNullOrEmpty(edtSoftInfo.Value))
                {
                    edtSoftInfo.Checked = false;
                }
                // End Added
            }
            else
            {
                edtSoftName.Checked = false;
                edtSoftName.Enabled = false;
            }
        }

        private void softVerSelector_SoftVersionSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.edtSoftInfo.Value = e.CustomObject;
        }

        private void softVerSelector_TrySelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.edtTry.Value = e.CustomObject;
        }

        public void edtInput_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.edtInput.Value.Trim() == string.Empty)
                {
                    ucMessageInfo.AddEx(this._FunctionName, "$Error_Date_Empty", new UserControl.Message(MessageType.Normal, "$Error_Date_Empty"), true);
                    this.edtInput.TextFocus(false, true);
                    return;
                }
                if (this.edtMO.Checked == true && this.edtMO.InnerTextBox.Enabled)
                {
                    ucMessageInfo.AddEx(this._FunctionName, "$CS_MO: " + this.edtMO.Value, new UserControl.Message(MessageType.Normal, "$CS_PleasePressEnterOnGOMO"), true);
                    this.edtMO.TextFocus(false, true);
                }
                else
                {
                    ucMessageInfo.AddWithoutEnter("<<");
                    ucMessageInfo.AddBoldText(edtInput.Value.Trim());
                    Collect();

                }
            }
        }

        #endregion

        #region 处理函数

        private bool GetOPBOMKeyparts()
        {
            OPBomKeyparts opBOMKeyparts = null;
            Messages msgMo = new Messages();

            try
            {
                //为改善性能               
                ((SQLDomainDataProvider)this.DataProvider).PersistBroker.AutoCloseConnection = false;
                ((SQLDomainDataProvider)this.DataProvider).PersistBroker.OpenConnection();

                string strMoCode = edtMO.Value.Trim().ToUpper();
                if (strMoCode == "")
                {
                    #region 归属工单相关
                    // Added by Icyer 2007/03/16		改为先判断是否需要归属工单，从待归属的工单中取产品代码；如果不归属工单才从Simulation取
                    ActionGoToMO actionGoMO = new ActionGoToMO(this.DataProvider);
                    msgMo = actionGoMO.GetItemCodeFromGoMoRCard(ApplicationService.Current().ResourceCode, _SourceRCard);
                    if (msgMo.IsSuccess() == false)		// 如果有错误，表示需要归属工单，但是解析工单或查询工单发生错误
                    {
                        ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, msgMo, true);
                        FlowControl = -1;
                        return false;
                    }
                    else	// 返回成功，有两种情况：需要归属工单并且返回正确的工单信息；不需要归属工单
                    {
                        UserControl.Message msgMoData = msgMo.GetData();
                        if (msgMoData != null)		// 有DATA数据，表示需要归属工单
                        {
                            MO mo = (MO)msgMoData.Values[0];
                            if (mo != null)
                                strMoCode = mo.MOCode;
                        }
                        else		// 如果没有DATA数据，表示不需要归属工单，则调用以前的代码：从序列号找产品
                        {
                            DataCollect.ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
                            Messages messages1 = onLine.GetIDInfo(_SourceRCard);
                            if (messages1.IsSuccess())
                            {
                                ProductInfo product = (ProductInfo)messages1.GetData().Values[0];
                                productInfo = product;
                                if (product.LastSimulation != null)
                                {
                                    strMoCode = product.LastSimulation.MOCode;
                                }
                                else if (productInfo.LastSimulation == null)
                                {
                                    ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, new UserControl.Message(MessageType.Error, "$NoSimulation"), true);
                                    FlowControl = -1;
                                    return false;
                                }
                            }
                        }
                    }
                    // Added end
                    #endregion
                }

                if (strMoCode != "")
                {
                    object[] objKeyParts = (object[])listOpBomKeyParts[strMoCode];
                    if (objKeyParts != null)
                    {
                        opBOMKeyparts = new OPBomKeyparts((object[])objKeyParts[0], Convert.ToInt32(objKeyParts[1]), this.DataProvider);

                        if (opBOMKeyparts != null && opBOMKeyparts.Count == 0)
                        {
                            ucMessageInfo.AddEx(objKeyParts[2].ToString());
                            FlowControl = -1;
                            return false;
                        }
                    }
                }

                if (opBOMKeyparts == null)
                {
                    DataCollect.ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
                    #region 先推途程，然后找OPBOM
                    ProductInfo product;
                    MO mo;
                    MOFacade moFacade = new MOFacade(this._domainDataProvider);
                    Messages messages1 = onLine.GetIDInfoByMoCodeAndId(strMoCode,_SourceRCard);
                    if (messages1.IsSuccess())
                    {
                        product = (ProductInfo)messages1.GetData().Values[0];
                        if (edtMO.Checked)
                        {
                            ActionGoToMO goToMO = new ActionGoToMO(this.DataProvider);
                            //AMOI  MARK  START  20050803  重复归属于同一工单时，不算出错
                            GoToMOActionEventArgs MOActionEventArgs = new GoToMOActionEventArgs(ActionType.DataCollectAction_GoMO, _SourceRCard, ApplicationService.Current().UserCode,
                                ApplicationService.Current().ResourceCode, product, edtMO.Value.Trim());
                            messages1 = goToMO.CheckIn(MOActionEventArgs);
                            if (!MOActionEventArgs.PassCheck)
                            {
                                messages1 = onLine.CheckID(new CKeypartsActionEventArgs(ActionType.DataCollectAction_CollectINNO, _SourceRCard, ApplicationService.Current().UserCode,
                                    ApplicationService.Current().ResourceCode, product, null, null));
                            }
                            //AMOI  MARK  END
                        }
                        else
                        {
                            /*	Removed by Icyer 2007/03/16
                            messages1= onLine.CheckID(new CKeypartsActionEventArgs(ActionType.DataCollectAction_CollectKeyParts,ID,ApplicationService.Current().UserCode,
                                ApplicationService.Current().ResourceCode,product,null,null));
                            */

                            // Added by Icyer 2007/03/16	如果归属工单，则做归属工单检查，否则做序列号途程检查
                            if (msgMo.GetData() != null)	// 需要归属工单，做归属工单检查
                            {
                                UserControl.Message msgMoData = msgMo.GetData();
                                mo = (MO)msgMoData.Values[0];
                                ActionGoToMO goToMO = new ActionGoToMO(this.DataProvider);
                                GoToMOActionEventArgs MOActionEventArgs = new GoToMOActionEventArgs(ActionType.DataCollectAction_GoMO, _SourceRCard, ApplicationService.Current().UserCode,
                                    ApplicationService.Current().ResourceCode, product, mo.MOCode);
                                messages1 = goToMO.CheckIn(MOActionEventArgs);
                                if (!MOActionEventArgs.PassCheck)
                                {
                                    messages1 = onLine.CheckID(new CKeypartsActionEventArgs(ActionType.DataCollectAction_CollectINNO, _SourceRCard, ApplicationService.Current().UserCode,
                                        ApplicationService.Current().ResourceCode, product, null, null));
                                }
                            }
                            else	// 不需要归属工单，检查序列号途程
                            {
                                messages1 = onLine.CheckID(new CKeypartsActionEventArgs(ActionType.DataCollectAction_CollectINNO, _SourceRCard, ApplicationService.Current().UserCode,
                                    ApplicationService.Current().ResourceCode, product, null, null));
                            }
                            if (messages1.IsSuccess() == false)
                            {
                                ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, messages1, true);
                                FlowControl = -1;
                                return false;
                            }
                            // Added end
                        }
                        if (messages1.IsSuccess())
                        {
                            //product=(ProductInfo)messages1.GetData().Values[0];
                            mo = (MO)moFacade.GetMO(product.NowSimulation.MOCode);
                        }
                        else
                        {
                            //Add By Bernard @ 2010-11-02
                            Messages messages2 = new Messages();
                            string exception = messages1.OutPut();
                            exception = exception.Replace(_SourceRCard.Trim().ToUpper(), this.edtInput.Value.Trim().ToUpper());
                            messages2.Add(new UserControl.Message(MessageType.Error, exception));
                            //end

                            ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, messages1, true);
                            FlowControl = -1;
                            return false;
                        }
                    }
                    else
                    {
                        //Add By Bernard @ 2010-11-02
                        Messages messages2 = new Messages();
                        string exception = messages1.OutPut();
                        exception = exception.Replace(_SourceRCard.Trim().ToUpper(), this.edtInput.Value.Trim().ToUpper());
                        messages2.Add(new UserControl.Message(MessageType.Error, exception));
                        //end

                        ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, messages1, true);
                        FlowControl = -1;
                        return false;
                    }



                    MO moNew = (MO)moFacade.GetMO(strMoCode);
                    OPBOMFacade opBOMFacade = new OPBOMFacade(this._domainDataProvider);
                    MaterialFacade materialFacade = new MaterialFacade(this._domainDataProvider);

                    _OPBOMDetailList = opBOMFacade.QueryOPBOMDetail(product.NowSimulation.ItemCode, string.Empty, string.Empty, moNew.BOMVersion,
                        product.NowSimulation.RouteCode, product.NowSimulation.OPCode, (int)MaterialType.CollectMaterial,
                        int.MinValue, int.MaxValue, moNew.OrganizationID, true);



                    if (_OPBOMDetailList == null || _OPBOMDetailList.Length <= 0)
                    {
                        ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, new UserControl.Message(MessageType.Error, "$CS_OPBOM_NotFound"), true);
                        ClearVariables();
                        return false;
                    }

                    _objBomDetailNotFilter = _OPBOMDetailList;
                    //过滤备选料
                    _OPBOMDetailList = FilterOPBOMDetail(_OPBOMDetailList);

                    if (_OPBOMDetailList == null || _OPBOMDetailList.Length <= 0)
                    {
                        ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, new UserControl.Message(MessageType.Error, "$CS_OPBOM_NotFound"), true);
                        ClearVariables();
                        return false;
                    }

                    //获取批控管上料资料
                    //注意：检查tblminno中的数据是否能对应到tblopbomdetail中
                    object[] tempMinNo = materialFacade.QueryMINNO_New(strMoCode, product.NowSimulation.RouteCode,
                        product.NowSimulation.OPCode, ApplicationService.Current().ResourceCode, moNew.BOMVersion);//modified Jarvis 20120321 for堆栈上料
                    if (tempMinNo == null)
                    {
                        _PreparedLotPartList = null;
                    }
                    else
                    {
                        ArrayList minnoList = new ArrayList();
                        foreach (MINNO minno in tempMinNo)
                        {
                            bool found = false;

                            if (_OPBOMDetailList != null)
                            {
                                foreach (OPBOMDetail opBOMDetail in _objBomDetailNotFilter)
                                {
                                    if (minno.MSourceItemCode == opBOMDetail.OPBOMSourceItemCode
                                        && opBOMDetail.OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_LOT
                                        && minno.MItemCode == opBOMDetail.OPBOMItemCode)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                            }
                            if (found)
                                minnoList.Add(minno);
                        }
                        _PreparedLotPartList = minnoList.ToArray();
                    }



                    _OPBOMDetailKeyPartList = opBOMFacade.GetOPBOMDetails(strMoCode,
                        product.NowSimulation.RouteCode, product.NowSimulation.OPCode, true, true);


                    //过滤备选料
                    if (_OPBOMDetailKeyPartList != null)
                    {
                        _OPBOMDetailKeyPartList = FilterOPBOMDetail(_OPBOMDetailKeyPartList);
                    }

                    opBOMKeyparts = new OPBomKeyparts(_OPBOMDetailList, Convert.ToInt32(mo.IDMergeRule), this.DataProvider);

                    if (_OPBOMDetailList != null && _OPBOMDetailList.Length > 0)
                    {
                        this.GetOPBOMDetailCount(strMoCode);
                    }

                    int _OPBOMDetailLotPartCount = 0;
                    if (_PreparedLotPartList != null)
                        _OPBOMDetailLotPartCount = _PreparedLotPartList.Length;

                    if (_OPBOMDetailKeyPartList != null && _NeedInputLotPart == false && _OPBOMDetailList.Length > (_OPBOMDetailKeyPartList.Length + _OPBOMDetailLotPartCount))
                    {
                        //ucMessageInfo.AddEx("$CS_LotControl_notFull ");
                        ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, new UserControl.Message(MessageType.Error, "$CS_LotControl_notFull"), true);
                        ClearVariables();
                        return false;
                    }

                    if (_OPBOMDetailKeyPartList == null && _NeedInputLotPart == false && _OPBOMDetailList.Length > _OPBOMDetailLotPartCount)
                    {
                        ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, new UserControl.Message(MessageType.Error, "$CS_LotControl_notFull"), true);
                        ClearVariables();
                        return false;
                    }

                    //if (opBomKeyparts.Count == 0)
                    //{
                    //    ucMessageInfo.AddEx("$CS_NOOPBomInfo $CS_Param_MOCode=" + product.NowSimulation.MOCode
                    //        + " $CS_Param_RouteCode=" + product.NowSimulation.RouteCode
                    //        + " $CS_Param_OPCode =" + product.NowSimulation.OPCode);
                    //    FlowControl = -1;
                    //    return false;
                    //}

                    // Added by Icyer 2005/11/01
                    if (!listOpBomKeyParts.ContainsKey(mo.MOCode))
                    {
                        string strMsg = "$CS_NOOPBomInfo $CS_Param_MOCode=" + product.NowSimulation.MOCode + " $CS_Param_RouteCode=" + product.NowSimulation.RouteCode + " $CS_Param_OPCode =" + product.NowSimulation.OPCode;
                        listOpBomKeyParts.Add(mo.MOCode, new object[] { _OPBOMDetailList, Convert.ToInt32(mo.IDMergeRule), strMsg });
                    }
                    // Added end
                    #endregion
                }


                //输出提示信息
                if (!_NeedInputLotPart && _OPBOMDetailKeyPartList == null)
                {

                }
                else
                {
                    if (this.checkMACID1.Checked)
                    {
                        ucMessageInfo.AddEx(">>$CS_PleaseInputMACID1");
                        return true;
                    }

                    if (this.checkMACID2.Checked)
                    {
                        ucMessageInfo.AddEx(">>$CS_PleaseInputMACID2");
                        return true;
                    }

                    if (this.checkMACID3.Checked)
                    {
                        ucMessageInfo.AddEx(">>$CS_PleaseInputMACID3");
                        return true;
                    }
                }



                if (_NeedInputLotPart == true && _OPBOMDetailList != null)
                {
                    if (((OPBOMDetail)_OPBOMDetailList[0]).OPBOMItemControlType == "item_control_lot")
                    {
                        ucMessageInfo.AddEx(">>$CS_PleaseInputLot>>$CS_Param_Lot=" + ((OPBOMDetail)_OPBOMDetailList[0]).OPBOMItemCode);
                    }
                    else
                    {
                        if (this.ckbSame.Checked == false)
                            ucMessageInfo.AddEx(">>$CS_PleaseInputKeypart >>$CS_Param_KeypartItem=" + ((OPBOMDetail)_OPBOMDetailList[0]).OPBOMItemCode);
                    }
                }
                else if (_NeedInputLotPart == false && _OPBOMDetailKeyPartList != null)
                {
                    if (this.ckbSame.Checked == false)
                        ucMessageInfo.AddEx(">>$CS_PleaseInputKeypart >>$CS_Param_KeypartItem=" + ((OPBOMDetail)_OPBOMDetailKeyPartList[0]).OPBOMItemCode);
                }
                else if (_NeedInputLotPart == false && _PreparedLotPartList == null && _OPBOMDetailKeyPartList == null)
                {
                    ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, new UserControl.Message(MessageType.Error, "$CS_NOOPBomInfo "), true);
                    ClearVariables();
                    return false;
                }




                return true;
            }
            catch (Exception ex)
            {
                ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, new UserControl.Message(ex), true);
                return false;
            }
            finally
            {
                ((SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
                ((SQLDomainDataProvider)this.DataProvider).PersistBroker.AutoCloseConnection = true;
            }
        }

        //获取ArrayList与Count
        private void GetOPBOMDetailCount(string moCode)
        {
            if (_OPBOMDetailList != null)
            {
                for (int i = 0; i < _OPBOMDetailList.Length; i++)
                {
                    if (((OPBOMDetail)_OPBOMDetailList[i]).OPBOMItemControlType == "item_control_lot")
                    {
                        _OPBOMDetailArrayList.Add(_OPBOMDetailList[i]);
                    }
                    else
                    {
                        int number = Convert.ToInt32(((OPBOMDetail)_OPBOMDetailList[i]).OPBOMItemQty);
                        for (int j = 0; j < number; j++)
                        {
                            _OPBOMDetailArrayList.Add(_OPBOMDetailList[i]);
                        }
                    }
                }

            }

            if (_OPBOMDetailKeyPartList != null)
            {
                for (int i = 0; i < _OPBOMDetailKeyPartList.Length; i++)
                {
                    int number = Convert.ToInt32(((OPBOMDetail)_OPBOMDetailKeyPartList[i]).OPBOMItemQty);//_SourceRCard

                    //获取连扳比例，Jarvis 20120323，先从TBLSMTRelationQty获取拼板比例，如果不存在则从TBLMO获取拼板比例
                    SMTFacade smtFacade = new SMTFacade(this._domainDataProvider);                    
                    object smtRelation = smtFacade.GetSMTRelationQty(_SourceRCard, moCode);
                    if (smtRelation != null)
                    {
                        number *= ((BenQGuru.eMES.Domain.SMT.Smtrelationqty)smtRelation).Relationqtry;
                    }
                    else
                    {
                        MOFacade moFacade = new MOFacade(this._domainDataProvider);
                        object mo = moFacade.GetMO(moCode);
                        number *= (int)(((MO)mo).IDMergeRule);
                    }

                    for (int j = 0; j < number; j++)
                    {
                        _OPBOMDetailKeyPartArrayList.Add(_OPBOMDetailKeyPartList[i]);
                    }
                }
            }

            if (_PreparedLotPartList != null)
            {
                for (int i = 0; i < _PreparedLotPartList.Length; i++)
                {
                    int number = Convert.ToInt32(((MINNO)_PreparedLotPartList[i]).Qty);
                    for (int j = 0; j < number; j++)
                    {
                        _PreparedLotPartArrayList.Add(_PreparedLotPartList[i]);
                    }
                }
            }
        }

        //采集
        private void Collect()
        {
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);

            UserControl.Messages messages = new UserControl.Messages();

            string data = Web.Helper.FormatHelper.CleanString(edtInput.Value.ToUpper().Trim());

            try
            {
                # region 初始提示信息

                if (FlowControl == -1)
                {
                    ucMessageInfo.AddEx(">>$CS_PleaseInputID");
                }

                #endregion

                #region 输入产品序列号后

                if (FlowControl == 0)
                {
                    _SourceRCard = dataCollectFacade.GetSourceCard(data, this.edtMO.Value.Trim());
                    #region 检查界面输入

                    if (this.edtMO.Checked && this.edtMO.InnerTextBox.Enabled)
                    {
                        ucMessageInfo.AddEx(this._FunctionName, "$CS_MO: " + this.edtMO.Value, new UserControl.Message(MessageType.Error, ">>$CS_PleasePressEnterOnGOMO"), true);
                        this.edtMO.Checked = true;
                        this.edtMO.TextFocus(false, true);
                        return;
                    }

                    if ((edtECN.Checked) && (edtECN.Value.Trim() == string.Empty))
                    {
                        ucMessageInfo.AddEx(">>$CS_CMPleaseInputECN");
                        edtECN.TextFocus(false, true);
                        return;
                    }

                    if ((edtSoftInfo.Checked) && (edtSoftInfo.Value.Trim() == string.Empty))
                    {
                        ucMessageInfo.AddEx(">>$CS_CMPleaseInputSoftInfo");
                        edtSoftInfo.TextFocus(false, true);
                        return;
                    }

                    if ((edtSoftName.Checked) && (edtSoftName.Value.Trim() == string.Empty))
                    {
                        ucMessageInfo.AddEx(">>$CS_CMPleaseInputSoftName");
                        edtSoftName.TextFocus(false, true);
                        return;
                    }

                    if ((edtTry.Checked) && (edtTry.Value.Trim() == string.Empty))
                    {
                        ucMessageInfo.AddEx(">>$CS_CMPleaseInputTry");
                        edtTry.TextFocus(false, true);
                        return;
                    }

                    //检查产品序列号格式
                    bool lenCheckBool = true;
                    //产品序列号长度检查
                    if (bRCardLenULE.Checked && bRCardLenULE.Value.Trim() != string.Empty)
                    {
                        int len = 0;
                        try
                        {
                            len = int.Parse(bRCardLenULE.Value.Trim());
                            if (data.Trim().Length != len)
                            {
                                lenCheckBool = false;
                                ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, new UserControl.Message(MessageType.Error, "$CS_Before_Card_Length_FLetter_NotCompare"), true);
                                ucMessageInfo.AddEx(">>$CS_PleaseInputID");
                                edtInput.TextFocus(false, true);
                                return;
                            }
                        }
                        catch
                        {
                            edtInput.TextFocus(false, true);
                            return;
                        }
                    }

                    //产品序列号首字符检查
                    if (bRCardLetterULE.Checked && bRCardLetterULE.Value.Trim() != string.Empty)
                    {
                        // Changed by Icyer 2006/11/13
                        int index = -1;
                        if (bRCardLetterULE.Value.Trim().ToUpper().Length <= data.Length)
                        {
                            index = data.IndexOf(bRCardLetterULE.Value.Trim().ToUpper());
                        }
                        // Changed end
                        if (index != 0)
                        {
                            lenCheckBool = false;
                            ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, new UserControl.Message(MessageType.Error, "$CS_Before_Card_FLetter_NotCompare"), true);
                            ucMessageInfo.AddEx(">>$CS_PleaseInputID");
                            edtInput.TextFocus(false, true);
                            return;
                        }
                    }


                    if (!this.SNConttentCheck(this.txtItemCode.Value.Trim().ToString(), this.edtInput.Value.Trim().ToString()))
                    {
                        ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, new UserControl.Message(MessageType.Error, "$CS_SNContent_CheckWrong $CS_Param_RunSeq:" + this.edtInput.Value.Trim().ToString()), true);
                        ucMessageInfo.AddEx(">>$CS_PleaseInputID");
                        edtInput.TextFocus(false, true);
                        return;
                    }

                    //报废不能返工 
                    Messages msg = new Messages();
                    msg.AddMessages(dataCollectFacade.CheckReworkRcardIsScarp(_SourceRCard.Trim(), ApplicationService.Current().ResourceCode));
                    if (!msg.IsSuccess())
                    {
                        ApplicationRun.GetInfoForm().AddEx(this._FunctionName, data.Trim() + ":" + data.Trim(), msg, true);
                        edtInput.TextFocus(false, true);
                        return;
                    }
                    //end

                    #endregion

                    //ID = data.Trim();

                    _NeedInputLotPart = (opsetCollectObject.CheckedItem.DataValue.ToString() == opCollectNeedInputLotPart);

                    if (!GetOPBOMKeyparts())
                    {
                        return;
                    }

                    //如果BOM中没有Keypart，并且自动采集Lot料的方式，则整个采集工作可自动完成
                    if (!_NeedInputLotPart && _OPBOMDetailKeyPartList == null)
                    {
                        if (this.checkMACID1.Checked)
                        {
                            ucMessageInfo.AddEx(">>$CS_PleaseInputMACID1");
                        }
                        else
                        {
                            if (this.checkMACID2.Checked)
                            {
                                ucMessageInfo.AddEx(">>$CS_PleaseInputMACID2");
                            }
                            else
                            {
                                if (this.checkMACID3.Checked)
                                {
                                    ucMessageInfo.AddEx(">>$CS_PleaseInputMACID3");
                                }
                                else
                                {
                                    if (AutoCollectAllLotPart())
                                    {
                                        Save();
                                        return;
                                    }
                                    else
                                    {
                                        ClearVariables();
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    //如果选中ckbSame，自动把产品序列号当成第一个KeyPart采集一次
                    if (this.ckbSame.Checked && _OPBOMDetailKeyPartArrayList != null & _OPBOMDetailKeyPartArrayList.Count > 0)
                    {
                        ArrayList opBOMDetailToCollect = new ArrayList();
                        if (opsetCollectObject.CheckedItem.DataValue.ToString() == opCollectAutoCollectLotPart)
                        {
                            opBOMDetailToCollect = _OPBOMDetailKeyPartArrayList;
                        }
                        else
                        {
                            opBOMDetailToCollect = _OPBOMDetailArrayList;
                        }

                        if (opBOMDetailToCollect.Count > 0)
                        {
                            if (CollectPart((OPBOMDetail)opBOMDetailToCollect[0], _SourceRCard))
                            {
                                if (!this.checkMACID1.Checked && !this.checkMACID2.Checked && !this.checkMACID3.Checked
                                    && _CollectedCount < opBOMDetailToCollect.Count)
                                {
                                    ShowMessageForPartInput((OPBOMDetail)opBOMDetailToCollect[_CollectedCount]);
                                }
                            }
                            else
                            {
                                ClearVariables();
                                return;
                            }
                        }
                    }
                }

                #endregion

                #region 输入物料后

                if (FlowControl >= 1)
                {
                    int IDCount = this.GetCheckIDCount();

                    if (IDCount > _CollectIDCount && !_HaveCollectMaterial)  //采集ID
                    {
                        this.CollectID(data.Trim());

                        if (IDCount == _CollectIDCount)  //最后提示输入料号
                        {
                            if (!_NeedInputLotPart && _OPBOMDetailKeyPartList == null)//自动采集：只有lot料没有keypart就自动完成上料
                            {
                                if (AutoCollectAllLotPart())
                                {
                                    Save();
                                    return;
                                }
                                else
                                {
                                    ClearVariables();
                                    return;
                                }
                            }

                            if (!_NeedInputLotPart && _OPBOMDetailKeyPartArrayList != null && _OPBOMDetailKeyPartArrayList.Count > 0
                                 && ckbSame.Checked && _CollectedCount == _OPBOMDetailKeyPartArrayList.Count)//自动采集且自动采第一个keypart：keypart采完,自动完成lot的采集。
                            {
                                if (AutoCollectAllLotPart())
                                {
                                    Save();
                                    return;
                                }
                                else
                                {
                                    ClearVariables();
                                    return;
                                }
                            }

                            if (_NeedInputLotPart && _OPBOMDetailArrayList != null && _OPBOMDetailArrayList.Count > 0
                                && this.ckbSame.Checked && _CollectedCount >= _OPBOMDetailArrayList.Count)//混合采集且自动采第一个keypart：只有一颗keypart料，上料完成。
                            {

                                Save();
                                return;
                            }

                            // 上料未完成，需要继续输入料号。
                            ArrayList opBOMDetailToCollect = new ArrayList();
                            if (opsetCollectObject.CheckedItem.DataValue.ToString() == opCollectAutoCollectLotPart)
                            {
                                opBOMDetailToCollect = _OPBOMDetailKeyPartArrayList;
                            }
                            else
                            {
                                opBOMDetailToCollect = _OPBOMDetailArrayList;
                            }
                            if (_CollectedCount < opBOMDetailToCollect.Count)
                            {
                                ShowMessageForPartInput((OPBOMDetail)opBOMDetailToCollect[_CollectedCount]);
                            }
                            _HaveCollectMaterial = true;
                            this.edtInput.TextFocus(true, true);
                            return;

                        }

                        this.edtInput.TextFocus(true, true);
                        return;
                    }
                    else//采集料号
                    {
                        ArrayList opBOMDetailToCollect = null;

                        if (opsetCollectObject.CheckedItem.DataValue.ToString() == opCollectAutoCollectLotPart)
                        {
                            opBOMDetailToCollect = _OPBOMDetailKeyPartArrayList;
                        }
                        else
                        {
                            opBOMDetailToCollect = _OPBOMDetailArrayList;
                        }

                        if (opBOMDetailToCollect != null)
                        {
                            if (_CollectedCount < opBOMDetailToCollect.Count)
                            {
                                try
                                {
                                    CollectPart((OPBOMDetail)opBOMDetailToCollect[_CollectedCount], data);
                                    if (_CollectedCount < opBOMDetailToCollect.Count)
                                    {
                                        ShowMessageForPartInput((OPBOMDetail)opBOMDetailToCollect[_CollectedCount]);
                                    }
                                }
                                catch (Exception e)
                                {
                                    ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, new UserControl.Message(e), true);
                                }
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                #endregion

                #region 采集物料后
                if (FlowControl >= 0)
                {
                    int countToCollect = GetCountToCollect();
                    int IDCount = this.GetCheckIDCount();

                    if (_CollectIDCount >= IDCount)
                    {
                        if (_CollectedCount >= countToCollect)
                        {
                            if (opsetCollectObject.CheckedItem.DataValue.ToString() == opCollectAutoCollectLotPart)
                            {
                                if (!AutoCollectAllLotPart())
                                {
                                    ClearVariables();
                                    return;
                                }
                            }

                            Save();
                            return;
                        }
                    }

                }
                #endregion

                edtInput.TextFocus(true, true);
                FlowControl++;
            }
            catch (Exception e)
            {
                messages.Add(new UserControl.Message(e));
                ClearVariables();
            }

            ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, messages, true);
        }

        //自动采集所有Lot物料
        private bool AutoCollectAllLotPart()
        {
            if (_PreparedLotPartList != null)
            {
                for (int i = 0; i < _PreparedLotPartList.Length; i++)
                {
                    try
                    {

                        string moCode = ((MINNO)_PreparedLotPartList[i]).MOCode;
                        string mItemCode = ((MINNO)_PreparedLotPartList[i]).MItemCode;
                        string barcode = ((MINNO)_PreparedLotPartList[i]).MItemPackedNo;

                        object opBomDetailNew = _objBomDetailNotFilter[0];
                        if (_objBomDetailNotFilter != null && _objBomDetailNotFilter.Length > 0)
                        {
                            for (int j = 0; j < _objBomDetailNotFilter.Length; j++)
                            {
                                if (((OPBOMDetail)_objBomDetailNotFilter[j]).OPBOMItemCode == mItemCode
                                    && ((OPBOMDetail)_objBomDetailNotFilter[j]).OPBOMSourceItemCode == ((MINNO)_PreparedLotPartList[i]).MSourceItemCode)
                                {
                                    opBomDetailNew = _objBomDetailNotFilter[j];
                                    break;
                                }
                            }
                        }

                        if (!CollectPart((OPBOMDetail)opBomDetailNew, barcode))
                        {
                            return false;
                        }

                    }
                    catch (Exception e)
                    {
                        ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, new UserControl.Message(e), true);
                    }
                }
            }

            return true;
        }

        //采集手工输入的物料
        private bool CollectPart(OPBOMDetail opBOMDetail, string materialSerialNo)
        {
            bool returnValue = false;
            Messages msg = new Messages();

            //检查新上料是否在TS中而不可用
            TSFacade tsFacade = new TSFacade(this.DataProvider);
            if (!tsFacade.RunningCardCanBeClollected(materialSerialNo, CardType.CardType_Part))
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$Error_Material_InTSOrScrapped $SERIAL_NO=" + materialSerialNo));
                ucMessageInfo.AddEx(this._FunctionName, "$CS_MCARD: " + materialSerialNo, msg, true);
                return returnValue;
            }

            try
            {
                //获取MOCode
                string moCode = this.edtMO.Value.ToString().Trim();
                if (moCode.Length <= 0)
                {
                    Simulation sim = (Simulation)_DataCollectFacade.GetSimulation(_SourceRCard);
                    if (sim != null)
                    {
                        moCode = sim.MOCode;
                    }
                }

                //解析获得MINNO
                MINNO newMINNO = _MaterialFacade.CreateNewMINNO();
                msg = _DataCollectFacade.GetMINNOByBarcode(opBOMDetail, materialSerialNo, moCode, _InputPartList, true, false, out newMINNO);

                if (msg.IsSuccess())
                {
                    if (newMINNO != null)
                    {
                        _InputPartList.Add((object)newMINNO);
                    }

                    ++_CollectedCount;
                    returnValue = true;
                }
                else
                {
                    ucMessageInfo.AddEx(this._FunctionName, "$CS_MCARD: " + materialSerialNo, msg, true);
                }

            }
            catch (Exception e)
            {
                ucMessageInfo.AddEx(this._FunctionName, "$CS_MCARD: " + materialSerialNo, new UserControl.Message(e), true);
            }

            return returnValue;
        }

        private void Save()
        {
            //add by hiro 2008/12/04 check try planqty >actualqty

            string OutputMessages = string.Empty;
            List<string> TryCodeList = new List<string>();
            TryFacade TryFacadeNew = new TryFacade(this.DataProvider);
            if (edtTry.Checked)
            {

                object[] TryList = TryFacadeNew.QueryTry(this.edtTry.Value.Trim().ToUpper());
                if (TryList != null)
                {
                    for (int i = 0; i < TryList.Length; i++)
                    {
                        if (((Try)TryList[i]).PlanQty <= ((Try)TryList[i]).ActualQty)
                        {
                            OutputMessages += MutiLanguages.ParserMessage("$Current_TryCode") + ": " + ((Try)TryList[i]).TryCode + "  " + MutiLanguages.ParserMessage("$PlanQty") + ((Try)TryList[i]).PlanQty + "  " + MutiLanguages.ParserMessage("$ActualQty") + ((Try)TryList[i]).ActualQty + "\r\n";
                        }
                    }
                }
            }

            #region 去掉隐形试流的数量的check
            //去掉隐形试流的数量的check changed by hiro  2009/01/19
            //if (_InputPartList != null)
            //{
            //    string ItemCode = this.txtItemCode.Value.ToString().Trim();
            //    if (ItemCode == string.Empty)
            //    {
            //        DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            //        object objectSimulation = dataCollectFacade.GetSimulation(ID);
            //        if (objectSimulation != null)
            //        {
            //            ItemCode = ((Simulation)objectSimulation).ItemCode;
            //        }
            //    }
            //    foreach (MINNO minno in _InputPartList)
            //    {
            //        object[] Try2RcardList = TryFacadeNew.QueryTry2RCard(string.Empty, minno.MItemPackedNo, minno.MItemCode);
            //        if (Try2RcardList != null)
            //        {
            //            foreach (Try2RCard try2RCard in Try2RcardList)
            //            {
            //                Try TryParent = (Try)TryFacadeNew.GetTry(try2RCard.TryCode);
            //                if (TryParent != null && TryParent.PlanQty <= TryParent.ActualQty)
            //                {
            //                    if (TryCodeList.Count == 0)
            //                    {
            //                        OutputMessages += "试流单号：" + TryParent.TryCode + "  计划数量是 " + TryParent.PlanQty + "  实际数量是 " + TryParent.ActualQty + "\r\n";
            //                    }
            //                    else
            //                    {
            //                        for (int i = 0; i < TryCodeList.Count; i++)
            //                        {
            //                            if (TryCodeList[0].ToString() != TryParent.TryCode)
            //                            {
            //                                OutputMessages += "试流单号：" + TryParent.TryCode + "  计划数量是 " + TryParent.PlanQty + "  实际数量是 " + TryParent.ActualQty + "\r\n";
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion

            if (OutputMessages.Length > 0)
            {
                if (MessageBox.Show(OutputMessages, MutiLanguages.ParserMessage("$ShowMessage"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtTry.Value, new UserControl.Message(MessageType.Error, "$PlanQty_big_QctualQty"), true);
                    ClearVariables();
                    return;
                }
            }
            //end 
            ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
            //DataProvider.BeginTransaction();
            //Laws Lu,2005/10/19,新增	缓解性能问题
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.BeginTransaction();
            // Added by Icyer 2005/10/28
            if (Resource == null)
            {
                BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
                Resource = (Domain.BaseSetting.Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);
            }
            // Added end
            try
            {
                DataCollectFacade _DataCollectFacade = new DataCollect.DataCollectFacade(this.DataProvider);
                ExtendSimulation lastSimulation = null;
                Messages _Messages = new Messages();
                ActionCheckStatus actionCheckStatus = new ActionCheckStatus();
                string strMoCode = edtMO.Value.Trim().ToUpper();
                ProductInfo currentProduct = null;
                if (strMoCode == "")
                {
                    /*	Removed by Icyer 2007/03/16
                    Messages messages1 = new Messages();
                    messages1 = onLine.GetIDInfo(ID);
                    if (messages1.IsSuccess())
                    {
                        currentProduct = (ProductInfo)messages1.GetData().Values[0];
                        if (currentProduct.LastSimulation != null)
                        {
                            strMoCode = currentProduct.LastSimulation.MOCode;
                        }
                        lastSimulation = currentProduct.LastSimulation;
                        currentProduct.Resource = Resource;
                    }
                    */
                    // Added by Icyer 2007/03/17		改为先判断是否需要归属工单，从待归属的工单中取产品代码；如果不归属工单才从Simulation取
                    ActionGoToMO actionGoMO = new ActionGoToMO(this.DataProvider);
                    Messages msgMo = actionGoMO.GetItemCodeFromGoMoRCard(ApplicationService.Current().ResourceCode, _SourceRCard);
                    if (msgMo.IsSuccess() == false)		// 如果有错误，表示需要归属工单，但是解析工单或查询工单发生错误
                    {
                    }
                    else	// 返回成功，有两种情况：需要归属工单并且返回正确的工单信息；不需要归属工单
                    {
                        UserControl.Message msgMoData = msgMo.GetData();
                        if (msgMoData != null)		// 有DATA数据，表示需要归属工单
                        {
                            MO mo = (MO)msgMoData.Values[0];
                            strMoCode = mo.MOCode;
                        }
                        else		// 如果没有DATA数据，表示不需要归属工单，则调用以前的代码：从序列号找产品
                        {
                            Messages messages1 = new Messages();
                            messages1 = onLine.GetIDInfo(_SourceRCard);
                            if (messages1.IsSuccess())
                            {
                                currentProduct = (ProductInfo)messages1.GetData().Values[0];
                                if (currentProduct.LastSimulation != null)
                                {
                                    strMoCode = currentProduct.LastSimulation.MOCode;
                                }
                                lastSimulation = currentProduct.LastSimulation;
                                currentProduct.Resource = Resource;
                            }
                        }
                    }
                    // Added end
                }
                if (strMoCode != "")
                {
                    if (listActionCheckStatus.ContainsKey(strMoCode))
                    {
                        actionCheckStatus = (ActionCheckStatus)listActionCheckStatus[strMoCode];
                        actionCheckStatus.ProductInfo = currentProduct;
                        actionCheckStatus.ActionList = new ArrayList();
                    }
                    else
                    {
                        actionCheckStatus.NeedUpdateSimulation = false;
                        actionCheckStatus.NeedFillReport = false;
                        listActionCheckStatus.Add(strMoCode, actionCheckStatus);
                    }
                }
                #region 各个保存
                //判断试流单
                if (this.edtTry.Checked)
                {
                    TryFacade tryfacade = new TryFacade(this.DataProvider);
                    object objTry = tryfacade.GetTry(this.edtTry.Value.Trim().ToUpper());
                    if (objTry != null)
                    {
                        if (((Try)objTry).Status != TryStatus.STATUS_PRODUCE && ((Try)objTry).Status != TryStatus.STATUS_RELEASE)
                        {
                            ucMessageInfo.AddEx(this._FunctionName, "$CS_TryLotCode: " + this.edtTry.Value, new UserControl.Message(MessageType.Error, "$Error_CS_Try_Should_be_Release_or_Open"), true);
                            this.edtTry.InnerTextBox.Enabled = false;
                            ClearVariables();
                            this.DataProvider.RollbackTransaction();
                            return;
                        }
                    }
                }
                if (edtMO.Checked)
                {
                    // Changed by Icyer 2005/10/18
                    //Messages messages1=  onLine.GetIDInfo(ID);
                    Messages messages1 = new Messages();
                    if (actionCheckStatus.ProductInfo == null)
                    {
                        messages1 = onLine.GetIDInfo(_SourceRCard);
                        actionCheckStatus.ProductInfo = (ProductInfo)messages1.GetData().Values[0];
                        actionCheckStatus.ProductInfo.Resource = Resource;
                        lastSimulation = actionCheckStatus.ProductInfo.LastSimulation;
                        _Messages.AddMessages(messages1);
                    }
                    // Changed end
                    if (_Messages.IsSuccess())
                    {
                        // Changed by Icyer 2005/10/18
                        //ProductInfo product=(ProductInfo)messages1.GetData().Values[0];
                        ProductInfo product = actionCheckStatus.ProductInfo;
                        // Changed end
                        messages1.AddMessages(onLine.ActionWithTransaction(new GoToMOActionEventArgs(ActionType.DataCollectAction_GoMO, _SourceRCard,
                            ApplicationService.Current().UserCode, ApplicationService.Current().ResourceCode,
                            product, edtMO.Value.Trim()), actionCheckStatus));
                    }
                    if (messages1.IsSuccess())
                        messages1.Add(new UserControl.Message(MessageType.Success, "$CS_GOMO_CollectSuccess"));
                    else
                    {
                        //ucMessageInfo.AddEx(messages1);
                        ClearVariables();

                    }
                    //ucMessageInfo.AddEx(messages1);	
                    _Messages.AddMessages(messages1);
                }

                BenQGuru.eMES.Material.WarehouseFacade wfacade = null;
                if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                {
                    wfacade = new WarehouseFacade(this.DataProvider);
                }

                //karron qiu
                //建议在上料采集界面和良品/不良品界面增加计数功能，当用户成功采集一个产品序列号后，
                //计数器加一，同时该计数功能允许用户归零。每次打开界面时，计数器基数都是零，系统不需要保存计数器值
                //bool flag = true;//标示是否采集成功

                if (opsetCollectObject.CheckedItem.DataValue.ToString() == opCollectNeedInputLotPart ||
                    (opsetCollectObject.CheckedItem.DataValue.ToString() == opCollectAutoCollectLotPart))
                {
                    //Messages messages1 = onLine.GetIDInfo(ID);
                    // Added by Icyer 2005/10/18
                    //actionCheckStatus.ProductInfo = (ProductInfo)messages1.GetData().Values[0];
                    //actionCheckStatus.ProductInfo.Resource = Resource;
                    Messages messages1 = new Messages();
                    if (actionCheckStatus.ProductInfo == null)
                    {
                        messages1 = onLine.GetIDInfo(_SourceRCard);
                        actionCheckStatus.ProductInfo = (ProductInfo)messages1.GetData().Values[0];
                        actionCheckStatus.ProductInfo.Resource = Resource;
                        lastSimulation = actionCheckStatus.ProductInfo.LastSimulation;
                        _Messages.AddMessages(messages1);
                    }
                    else	//将上一个Action的NowSimulation设置为本Action的LastSimulation
                    {
                        if (actionCheckStatus.ActionList.Count > 0)
                        {
                            actionCheckStatus.ProductInfo = new ProductInfo();
                            actionCheckStatus.ProductInfo.NowSimulation = new Simulation();
                            actionCheckStatus.ProductInfo.Resource = Resource;
                            actionCheckStatus.ProductInfo.LastSimulation =
                                new ExtendSimulation(((ActionEventArgs)actionCheckStatus.ActionList[actionCheckStatus.ActionList.Count - 1]).ProductInfo.NowSimulation);

                        }
                    }

                    // Changed end
                    if (_Messages.IsSuccess())
                    {
                        #region Check if key part was used

                        if (_InputPartList != null)
                        {
                            foreach (MINNO minno in _InputPartList)
                            {
                                if (minno.EAttribute1 == MCardType.MCardType_Keyparts)
                                {
                                    if (_DataCollectFacade.KeyPartUsed(minno.MItemPackedNo, minno.MItemCode, false, _InputPartList))
                                    {
                                        messages1.Add(new UserControl.Message(MessageType.Error, "$CS_Error_KeyPartUsed"));
                                        break;
                                    }

                                }
                            }

                        }

                        #endregion

                        if (messages1.IsSuccess())
                        {
                            // Changed by Icyer 2005/10/18
                            ProductInfo product = actionCheckStatus.ProductInfo;
                            // Changed end
                            object[] objBomDetailLot = new object[_InputPartList.Count];
                            _InputPartList.CopyTo(objBomDetailLot);
                            if (objBomDetailLot != null)
                            {
                                messages1.AddMessages(onLine.ActionWithTransaction(new CINNOActionEventArgs(ActionType.DataCollectAction_CollectINNO, _SourceRCard,
                                    ApplicationService.Current().UserCode, ApplicationService.Current().ResourceCode,
                                    product, INNO, wfacade
                                    ), actionCheckStatus, objBomDetailLot));

                            }
                        }
                    }
                    if (messages1.IsSuccess())
                        messages1.Add(new UserControl.Message(MessageType.Success, "$CS_CollectSuccess"));

                    _Messages.AddMessages(messages1);
                }

                //如果使用Material部分，则执行缓存的SQL
                if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                {
                    if (wfacade != null)
                        wfacade.ExecCacheSQL();
                }

                if (edtECN.Checked)
                {
                    // Changed by Icyer 2005/10/18
                    //Messages messages1=  onLine.GetIDInfo(ID);
                    Messages messages1 = new Messages();
                    if (actionCheckStatus.ProductInfo == null)
                    {
                        messages1 = onLine.GetIDInfo(_SourceRCard);
                        actionCheckStatus.ProductInfo = (ProductInfo)messages1.GetData().Values[0];
                        actionCheckStatus.ProductInfo.Resource = Resource;
                        lastSimulation = actionCheckStatus.ProductInfo.LastSimulation;
                        _Messages.AddMessages(messages1);
                    }
                    else	//将上一个Action的NowSimulation设置为本Action的LastSimulation
                    {
                        if (actionCheckStatus.ActionList.Count > 0)
                        {
                            actionCheckStatus.ProductInfo = new ProductInfo();
                            actionCheckStatus.ProductInfo.NowSimulation = new Simulation();
                            actionCheckStatus.ProductInfo.Resource = Resource;
                            actionCheckStatus.ProductInfo.LastSimulation =
                                new ExtendSimulation(((ActionEventArgs)actionCheckStatus.ActionList[actionCheckStatus.ActionList.Count - 1]).ProductInfo.NowSimulation);

                        }
                    }
                    // Changed end
                    if (_Messages.IsSuccess())
                    {
                        // Changed by Icyer 2005/10/18
                        //ProductInfo product=(ProductInfo)messages1.GetData().Values[0];
                        ProductInfo product = actionCheckStatus.ProductInfo;
                        // Changed end
                        messages1.AddMessages(onLine.ActionWithTransaction(new EcnTryActionEventArgs(ActionType.DataCollectAction_ECN, _SourceRCard,
                            ApplicationService.Current().UserCode, ApplicationService.Current().ResourceCode,
                            product, edtECN.Value.Trim(), edtTry.Value.Trim()), actionCheckStatus));
                    }
                    if (messages1.IsSuccess())
                        messages1.Add(new UserControl.Message(MessageType.Success, "$CS_ECNorTry_CollectSuccess"));

                    //ucMessageInfo.AddEx(messages1);

                    _Messages.AddMessages(messages1);
                }
                if ((edtSoftInfo.Checked) || (edtSoftName.Checked))
                {
                    // Changed by Icyer 2005/10/18
                    //Messages messages1=  onLine.GetIDInfo(ID);
                    Messages messages1 = new Messages();
                    if (actionCheckStatus.ProductInfo == null)
                    {
                        messages1 = onLine.GetIDInfo(_SourceRCard);
                        actionCheckStatus.ProductInfo = (ProductInfo)messages1.GetData().Values[0];
                        actionCheckStatus.ProductInfo.Resource = Resource;
                        lastSimulation = actionCheckStatus.ProductInfo.LastSimulation;
                        _Messages.AddMessages(messages1);
                    }
                    else	//将上一个Action的NowSimulation设置为本Action的LastSimulation
                    {
                        if (actionCheckStatus.ActionList.Count > 0)
                        {
                            actionCheckStatus.ProductInfo = new ProductInfo();
                            actionCheckStatus.ProductInfo.NowSimulation = new Simulation();
                            actionCheckStatus.ProductInfo.Resource = Resource;
                            actionCheckStatus.ProductInfo.LastSimulation =
                                new ExtendSimulation(((ActionEventArgs)actionCheckStatus.ActionList[actionCheckStatus.ActionList.Count - 1]).ProductInfo.NowSimulation);

                        }
                    }
                    // Changed end
                    if (_Messages.IsSuccess())
                    {
                        // Changed by Icyer 2005/10/18
                        //ProductInfo product=(ProductInfo)messages1.GetData().Values[0];
                        ProductInfo product = actionCheckStatus.ProductInfo;
                        // Changed end
                        messages1.AddMessages(onLine.ActionWithTransaction(new SoftwareActionEventArgs(ActionType.DataCollectAction_SoftINFO, _SourceRCard,
                            ApplicationService.Current().UserCode, ApplicationService.Current().ResourceCode,
                            product, edtSoftInfo.Value.Trim(), edtSoftName.Value.Trim()), actionCheckStatus));
                    }
                    if (messages1.IsSuccess())
                        messages1.Add(new UserControl.Message(MessageType.Success, "$CS_Soft_CollectSuccess"));

                    //ucMessageInfo.AddEx(messages1);

                    _Messages.AddMessages(messages1);
                }

                #region SaveIDInfo
                if (this.GetCheckIDCount() > 0)
                {
                    //保存前的检查
                    if (_Messages.IsSuccess())
                    {
                        Messages newMessage = new Messages();

                        if (!string.IsNullOrEmpty(_IDInfo.ID1))
                        {
                            if (!this.CheckIDIsOnly(_IDInfo.ID1.Trim(), 1))
                            {
                                newMessage.Add(new UserControl.Message(MessageType.Error, "$CS_IDIsNotOnly"));
                                _Messages.AddMessages(newMessage);
                            }
                        }
                    }

                    if (_Messages.IsSuccess())
                    {
                        Messages newMessage = new Messages();

                        if (!string.IsNullOrEmpty(_IDInfo.ID2))
                        {
                            if (!this.CheckIDIsOnly(_IDInfo.ID2.Trim(), 2))
                            {
                                newMessage.Add(new UserControl.Message(MessageType.Error, "$CS_IDIsNotOnly"));
                                _Messages.AddMessages(newMessage);
                            }
                        }
                    }

                    if (_Messages.IsSuccess())
                    {
                        Messages newMessage = new Messages();

                        if (!string.IsNullOrEmpty(_IDInfo.ID3))
                        {
                            if (!this.CheckIDIsOnly(_IDInfo.ID3.Trim(), 3))
                            {
                                newMessage.Add(new UserControl.Message(MessageType.Error, "$CS_IDIsNotOnly"));
                                _Messages.AddMessages(newMessage);
                            }
                        }
                    }

                    //end

                    if (_Messages.IsSuccess())
                    {
                        Messages messages1 = new Messages();
                        if (actionCheckStatus.ProductInfo == null)
                        {
                            messages1 = onLine.GetIDInfo(_SourceRCard);
                            actionCheckStatus.ProductInfo = (ProductInfo)messages1.GetData().Values[0];
                            actionCheckStatus.ProductInfo.Resource = Resource;
                            lastSimulation = actionCheckStatus.ProductInfo.LastSimulation;
                            _Messages.AddMessages(messages1);
                        }
                        else	//将上一个Action的NowSimulation设置为本Action的LastSimulation
                        {
                            if (actionCheckStatus.ActionList.Count > 0)
                            {
                                actionCheckStatus.ProductInfo = new ProductInfo();
                                actionCheckStatus.ProductInfo.NowSimulation = new Simulation();
                                actionCheckStatus.ProductInfo.Resource = Resource;
                                actionCheckStatus.ProductInfo.LastSimulation =
                                    new ExtendSimulation(((ActionEventArgs)actionCheckStatus.ActionList[actionCheckStatus.ActionList.Count - 1]).ProductInfo.NowSimulation);

                            }
                        }

                        DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                        DataCollect.DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                        TSFacade tsFacade = new TSFacade(this.DataProvider);
                        MOFacade moFacade = new MOFacade(this.DataProvider);
                        BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);

                        Domain.TS.TS objectTS = (Domain.TS.TS)tsFacade.QueryLastTSByRunningCard(_SourceRCard.Trim().ToUpper());

                        Simulation LastSimulation = (Simulation)dataCollectFacade.GetSimulation(_SourceRCard.Trim().ToUpper());

                        if (this.edtMO.Value.Trim() != string.Empty)
                        {
                            _IDInfo.MOCode = this.edtMO.Value.Trim();
                        }
                        else
                        {
                            if (LastSimulation != null)
                            {
                                _IDInfo.MOCode = LastSimulation.MOCode;
                            }
                        }


                        if (LastSimulation != null)
                        {
                            _IDInfo.RouteCode = LastSimulation.RouteCode;
                        }
                        else
                        {
                            MO2Route getMO2Route = (MO2Route)moFacade.GetMONormalRouteByMOCode(_IDInfo.MOCode);
                            if (getMO2Route != null)
                            {
                                _IDInfo.RouteCode = getMO2Route.RouteCode;
                            }
                            else
                            {
                                _IDInfo.RouteCode = " ";
                            }

                        }

                        _IDInfo.RCard = _SourceRCard.Trim().ToUpper();
                        _IDInfo.RCardSeq = Convert.ToInt32(actionCheckStatus.ProductInfo.LastSimulation.RunningCardSequence);
                        _IDInfo.ResourceCode = ApplicationService.Current().ResourceCode;

                        Route2Operation route2Operation = (Route2Operation)baseModelFacade.GetOPFromRoute2OP(_IDInfo.RouteCode, _IDInfo.ResourceCode);
                        if (route2Operation != null)
                        {
                            _IDInfo.OPCode = route2Operation.OPCode;
                        }
                        else
                        {
                            _IDInfo.OPCode = " ";
                        }
                        _IDInfo.SSCode = ApplicationService.Current().LoginInfo.Resource.StepSequenceCode;
                        _IDInfo.SegmentCode = ApplicationService.Current().LoginInfo.Resource.SegmentCode;
                        _IDInfo.MaintainUser = ApplicationService.Current().UserCode;
                        _IDInfo.MaintainDate = dBDateTime.DBDate;
                        _IDInfo.MaintainTime = dBDateTime.DBTime;

                        IDInfo objectIDInfo = (IDInfo)dataCollectFacade.GetIDInfo(_IDInfo.MOCode, _IDInfo.RCard);

                        if (objectIDInfo != null)
                        {
                            dataCollectFacade.DeleteIDInfo(objectIDInfo);
                            dataCollectFacade.AddIDInfo(_IDInfo);
                        }
                        else
                        {
                            dataCollectFacade.AddIDInfo(_IDInfo);
                        }
                    }
                }

                #endregion


                //更新try2rcard and try add by hiro 08/11/10
                //物料过账
                BaseModelFacade bMfacade = new BaseModelFacade(this.DataProvider);
                object objOP = bMfacade.GetOperationByResource(ApplicationService.Current().ResourceCode);
                if (_InputPartList != null)
                {
                    Messages messagesNew = new Messages();
                    string ItemCode = this.txtItemCode.Value.ToString().Trim();
                    if (ItemCode == string.Empty)
                    {
                        DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                        object objectSimulation = dataCollectFacade.GetSimulation(_SourceRCard);
                        if (objectSimulation != null)
                        {
                            ItemCode = ((Simulation)objectSimulation).ItemCode;
                        }
                    }
                    foreach (MINNO minno in _InputPartList)
                    {
                        messagesNew.AddMessages(onLine.ActionWithTransaction(new TryEventArgs(
                            ActionType.DataCollectAction_TryNew, ApplicationService.Current().UserCode, ((Operation2Resource)objOP).OPCode, ApplicationService.Current().ResourceCode,
                            ItemCode, _SourceRCard, minno.MItemCode, minno.MItemPackedNo, string.Empty, true, true)));
                    }
                    _Messages.AddMessages(messagesNew);
                }
                //成品过账
                if (edtTry.Checked)
                {
                    Messages messagesNew = new Messages();
                    string ItemCode = this.txtItemCode.Value.ToString().Trim();
                    if (ItemCode == string.Empty)
                    {
                        DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                        object objectSimulation = dataCollectFacade.GetSimulation(_SourceRCard);
                        if (objectSimulation != null)
                        {
                            ItemCode = ((Simulation)objectSimulation).ItemCode;
                        }
                    }
                    messagesNew.AddMessages(onLine.ActionWithTransaction(new TryEventArgs(
                        ActionType.DataCollectAction_TryNew, ApplicationService.Current().UserCode, ((Operation2Resource)objOP).OPCode, ApplicationService.Current().ResourceCode,
                        ItemCode, _SourceRCard, string.Empty, string.Empty, this.edtTry.Value.Trim(), true, true)));

                    _Messages.AddMessages(messagesNew);
                }
                //end


                //AMOI  MARK  START  20050803 如果OP设定中有测试工序，则必须作测试,为避免多次推途程，直接作GOOD采集，如果出现OP设定错误，则过滤
                {
                    //Messages messages1 = onLine.GetIDInfo(ID);
                    // Added by Icyer 2005/10/18
                    //actionCheckStatus.ProductInfo = (ProductInfo)messages1.GetData().Values[0];
                    //actionCheckStatus.ProductInfo.Resource = Resource;
                    Messages messages1 = new Messages();
                    if (actionCheckStatus.ProductInfo == null)
                    {
                        messages1 = onLine.GetIDInfo(_SourceRCard);
                        actionCheckStatus.ProductInfo = (ProductInfo)messages1.GetData().Values[0];
                        actionCheckStatus.ProductInfo.Resource = Resource;
                        lastSimulation = actionCheckStatus.ProductInfo.LastSimulation;
                        _Messages.AddMessages(messages1);
                    }
                    else	//将上一个Action的NowSimulation设置为本Action的LastSimulation
                    {
                        if (actionCheckStatus.ActionList.Count > 0)
                        {
                            actionCheckStatus.ProductInfo = new ProductInfo();
                            actionCheckStatus.ProductInfo.NowSimulation = new Simulation();
                            actionCheckStatus.ProductInfo.Resource = Resource;
                            actionCheckStatus.ProductInfo.LastSimulation =
                                new ExtendSimulation(((ActionEventArgs)actionCheckStatus.ActionList[actionCheckStatus.ActionList.Count - 1]).ProductInfo.NowSimulation);
                        }
                    }
                    // Changed end
                    if (_Messages.IsSuccess())
                    {
                        // Changed by Icyer 2005/10/18
                        //ProductInfo product=(ProductInfo)messages1.GetData().Values[0];
                        ProductInfo product = actionCheckStatus.ProductInfo;
                        // Changed end

                        messages1.AddMessages(onLine.ActionWithTransaction(new ActionEventArgs(ActionType.DataCollectAction_GOOD, _SourceRCard,
                            ApplicationService.Current().UserCode, ApplicationService.Current().ResourceCode,
                            product), actionCheckStatus));
                    }
                    if (_Messages.IsSuccess())
                    {
                        // Added by Icyer 2005/10/31
                        // 更新Wip & Simulation
                        ActionEventArgs actionEventArgs;
                        actionEventArgs = (ActionEventArgs)actionCheckStatus.ActionList[actionCheckStatus.ActionList.Count - 1];
                        ExtendSimulation oldLastSimulation = actionEventArgs.ProductInfo.LastSimulation;
                        actionEventArgs.ProductInfo.LastSimulation = lastSimulation;
                        _Messages.AddMessages(onLine.Execute(actionEventArgs, actionCheckStatus, true, false));
                        actionEventArgs.ProductInfo.LastSimulation = oldLastSimulation;

                        ReportHelper reportCollect = new ReportHelper(this.DataProvider);
                        for (int i = 0; i < actionCheckStatus.ActionList.Count; i++)
                        {
                            actionEventArgs = (ActionEventArgs)actionCheckStatus.ActionList[i];
                            //更新WIP
                            if (actionEventArgs.OnWIP != null)
                            {
                                for (int iwip = 0; iwip < actionEventArgs.OnWIP.Count; iwip++)
                                {
                                    if (actionEventArgs.OnWIP[iwip] is OnWIP)
                                    {
                                        _DataCollectFacade.AddOnWIP((OnWIP)actionEventArgs.OnWIP[iwip]);
                                    }
                                    //								else if (actionEventArgs.OnWIP[iwip] is OnWIPItem)
                                    //								{
                                    //									_DataCollectFacade.AddOnWIPItem((OnWIPItem)actionEventArgs.OnWIP[iwip]);
                                    //								}
                                    else if (actionEventArgs.OnWIP[iwip] is OnWIPECN)
                                    {
                                        _DataCollectFacade.AddOnWIPECN((OnWIPECN)actionEventArgs.OnWIP[iwip]);
                                    }
                                    else if (actionEventArgs.OnWIP[iwip] is OnWIPTRY)
                                    {
                                        _DataCollectFacade.AddOnWIPTRY((OnWIPTRY)actionEventArgs.OnWIP[iwip]);
                                    }
                                    else if (actionEventArgs.OnWIP[iwip] is OnWIPSoftVersion)
                                    {
                                        _DataCollectFacade.AddOnWIPSoftVersion((OnWIPSoftVersion)actionEventArgs.OnWIP[iwip]);
                                    }
                                }
                            }
                        }
                        //根据Action类型更新Report
                        //for (int i = 0; i < actionCheckStatus.ActionList.Count; i++)
                        //{
                        //    actionEventArgs = (ActionEventArgs)actionCheckStatus.ActionList[i];
                        //    if (actionCheckStatus.NeedFillReport == false)
                        //    {
                        //        if (actionEventArgs.ActionType == ActionType.DataCollectAction_GoMO)
                        //        {
                        //            _Messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo, actionCheckStatus));
                        //        }
                        //        else if (actionEventArgs.ActionType == ActionType.DataCollectAction_CollectINNO || actionEventArgs.ActionType == ActionType.DataCollectAction_CollectKeyParts)
                        //        {
                        //            _Messages.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo, actionCheckStatus));
                        //        }
                        //        else if (actionEventArgs.ActionType == ActionType.DataCollectAction_GOOD)
                        //        {
                        //            _Messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo, actionCheckStatus));
                        //            _Messages.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo, actionCheckStatus));
                        //        }
                        //    }
                        //}
                        // Added end
                        messages1.Add(new UserControl.Message(MessageType.Success, "$CS_GOOD_CollectSuccess"));
                        _Messages.AddMessages(messages1);
                    }
                    else
                    {
                        bool f = false;
                        for (int i = 0; i < messages1.Count(); i++)
                        {
                            UserControl.Message message2 = messages1.Objects(i);
                            if (message2.Type == MessageType.Error)
                            {
                                if (message2.Body.IndexOf("$CS_OP_Not_TestOP") < 0)
                                {
                                    if (message2.Exception != null)
                                        if (message2.Exception.Message.IndexOf("$CS_OP_Not_TestOP") >= 0)
                                            continue;
                                    f = true;
                                }
                            }
                        }
                        _Messages.AddMessages(messages1);
                    }
                }
                //AMOI  MARK  END
                #endregion

                if (_Messages.IsSuccess())
                {
                    //DataProvider.CommitTransaction();
                    this.AddCollectedCount();

                    ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD:" + this.edtInput.Value, _Messages, true);
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CommitTransaction();
                }
                else
                {
                    //DataProvider.RollbackTransaction();

                    UserControl.Messages message = new UserControl.Messages();
                    for (int i = 0; i < _Messages.Count(); i++)
                    {
                        if (_Messages.Objects(i).Type == MessageType.Error)
                        {
                            message.Add(_Messages.Objects(i));
                        }
                    }

                    ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, message, true);

                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
                }

            }
            catch (Exception e)
            {
                //DataProvider.RollbackTransaction();
                ucMessageInfo.AddEx(this._FunctionName, "$CS_RCARD: " + this.edtInput.Value, new UserControl.Message(e), true);
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();

            }
            finally
            {
                //Laws Lu,2005/10/19,新增	缓解性能问题
                //				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                //Laws Lu,2007/01/05,新增	缓解性能问题
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
            }

            ClearVariables();

        }

        private void ClearVariables()
        {
            FlowControl = -1;

            _IDInfo = new IDInfo();
            _OPBOMDetailArrayList = new ArrayList();
            _OPBOMDetailKeyPartArrayList = new ArrayList();

            _PreparedLotPartArrayList = new ArrayList();

            _CollectedCount = 0;
            _CollectIDCount = 0;
            _HaveCollectID1 = false;
            _HaveCollectID2 = false;
            _HaveCollectID3 = false;
            _HaveCollectMaterial = false;
            _SourceRCard = string.Empty;

            _InputPartList = new ArrayList();
            listOpBomKeyParts = new Hashtable();
        }

        private void ShowMessageForPartInput(OPBOMDetail opBOMDetail)
        {
            if (opBOMDetail.OPBOMItemControlType == "item_control_keyparts")
            {
                ucMessageInfo.AddEx(">>$CS_PleaseInputKeypart >>$CS_Param_KeypartItem=" + opBOMDetail.OPBOMItemCode);
            }
            else if (opBOMDetail.OPBOMItemControlType == "item_control_lot")
            {
                ucMessageInfo.AddEx(">>$CS_PleaseInputLot>>$CS_Param_Lot=" + opBOMDetail.OPBOMItemCode);
            }
        }

        private object[] FilterOPBOMDetail(object[] bomDetailList)
        {
            ArrayList filterList = new ArrayList();

            for (int i = 0; i < bomDetailList.Length; i++)
            {
                if (((OPBOMDetail)bomDetailList[i]).OPBOMItemControlType != BOMItemControlType.ITEM_CONTROL_KEYPARTS
                    && ((OPBOMDetail)bomDetailList[i]).OPBOMItemControlType != BOMItemControlType.ITEM_CONTROL_LOT)
                {
                    continue;
                }

                bool found = false;
                for (int j = 0; j < filterList.Count; j++)
                {
                    if (((OPBOMDetail)bomDetailList[i]).OPBOMSourceItemCode == ((OPBOMDetail)filterList[j]).OPBOMSourceItemCode)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    filterList.Add(bomDetailList[i]);
                }
            }

            return filterList.ToArray();
        }

        //检查序列号内容为字母,数字和空格
        private bool SNConttentCheck(string itemCode, string rCard)
        {
            bool returnValue = true;

            ItemFacade itemFacade = new ItemFacade(this.DataProvider);

            string pattern = @"^([A-Za-z0-9]+[ ]*)*[A-Za-z0-9]+$";
            Regex rex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = rex.Match(rCard);

            object obj = itemFacade.GetItem2SNCheck(itemCode, ItemCheckType.ItemCheckType_SERIAL);
            if (obj != null && ((Item2SNCheck)obj).SNContentCheck == "Y" && !match.Success)
            {
                returnValue = false;
            }

            return returnValue;
        }

        private bool IsNumber(object obj)
        {
            try
            {
                int.Parse(obj.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void AddCollectedCount()
        {
            this.CollectedCount.Text = Convert.ToString(Convert.ToInt32(this.CollectedCount.Text) + 1);
            ApplicationRun.GetQtyForm().RefreshQty();
        }

        private void ClearCollectedCount()
        {
            this.CollectedCount.Text = "0";
        }

        private int GetCountToCollect()
        {
            int returnValue = 0;

            if (opsetCollectObject.CheckedItem.DataValue.ToString() == opCollectAutoCollectLotPart)
            {
                if (_OPBOMDetailKeyPartArrayList != null)
                {
                    returnValue = _OPBOMDetailKeyPartArrayList.Count;
                }
            }
            else
            {
                if (_OPBOMDetailArrayList != null)
                {
                    returnValue = _OPBOMDetailArrayList.Count;
                }
            }

            return returnValue;
        }


        private int GetCheckIDCount()
        {
            int num = 0;

            if (this.checkMACID1.Checked)
            {
                num += 1;
            }

            if (this.checkMACID2.Checked)
            {
                num += 1;
            }

            if (this.checkMACID3.Checked)
            {
                num += 1;
            }
            return num;
        }

        private void CollectID(string idData)
        {
            if (this.checkMACID1.Checked && !_HaveCollectID1)
            {
                bool checkId = this.CheckIDIsOnly(idData, 1);

                if (!checkId)
                {
                    ucMessageInfo.AddEx(this._FunctionName, this.edtInput.Value, new UserControl.Message(MessageType.Error, "$CS_IDIsNotOnly "), true);
                    ucMessageInfo.AddEx(">>$CS_PleaseInputMACID1");
                    return;
                }

                if (!CheckIDIsTrue(idData, 1))
                {
                    ucMessageInfo.AddEx(">>$CS_PleaseInputMACID1");
                    return;
                }

                _IDInfo.ID1 = idData;

                _HaveCollectID1 = true;
                _CollectIDCount += 1;

                if (this.checkMACID2.Checked && !_HaveCollectID2)
                {
                    ucMessageInfo.AddEx(">>$CS_PleaseInputMACID2");
                }
                else
                {
                    if (this.checkMACID3.Checked && !_HaveCollectID3)
                    {
                        ucMessageInfo.AddEx(">>$CS_PleaseInputMACID3");
                    }
                }

                return;

            }


            if (this.checkMACID2.Checked && !_HaveCollectID2)
            {
                bool checkId = this.CheckIDIsOnly(idData, 2);

                if (!checkId)
                {
                    ucMessageInfo.AddEx(this._FunctionName, this.edtInput.Value, new UserControl.Message(MessageType.Error, "$CS_IDIsNotOnly "), true);
                    ucMessageInfo.AddEx(">>$CS_PleaseInputMACID2");
                    return;
                }

                if (!CheckIDIsTrue(idData, 2))
                {
                    ucMessageInfo.AddEx(">>$CS_PleaseInputMACID2");
                    return;
                }

                _IDInfo.ID2 = idData;

                _HaveCollectID2 = true;
                _CollectIDCount += 1;

                if (this.checkMACID3.Checked)
                {
                    ucMessageInfo.AddEx(">>$CS_PleaseInputMACID3");
                }


                return;
            }

            if (this.checkMACID3.Checked && !_HaveCollectID3)
            {
                bool checkId = this.CheckIDIsOnly(idData, 3);

                if (!checkId)
                {
                    ucMessageInfo.AddEx(this._FunctionName, this.edtInput.Value, new UserControl.Message(MessageType.Error, "$CS_IDIsNotOnly "), true);
                    ucMessageInfo.AddEx(">>$CS_PleaseInputMACID3");
                    return;
                }

                if (!CheckIDIsTrue(idData, 3))
                {
                    ucMessageInfo.AddEx(">>$CS_PleaseInputMACID3");
                    return;
                }

                _IDInfo.ID3 = idData;

                _HaveCollectID3 = true;
                _CollectIDCount += 1;

                return;
            }
        }

        private bool CheckIDIsOnly(string idData, int number)
        {
            DataCollect.DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);

            IDInfo getIDInfo = (IDInfo)dataCollectFacade.GetIDInfo(string.Empty, string.Empty, idData, number);

            if (getIDInfo != null)
            {
                if (getIDInfo.RCard.Trim().ToUpper() != _SourceRCard.Trim().ToUpper())
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckIDIsTrue(string idData, int idNo)
        {
            string itemCode = FormatHelper.CleanString(this.txtItemCode.Value.Trim().ToUpper());
            if (itemCode == string.Empty)
            {
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                Simulation simulation = (Simulation)dataCollectFacade.GetSimulation(_SourceRCard);
                if (simulation != null)
                {
                    itemCode = simulation.ItemCode;
                }
            }

            ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            string id = "ID" + idNo.ToString();
            Item2SNCheck item2SNCheck = (Item2SNCheck)itemFacade.GetItem2SNCheck(itemCode, id);

            if (item2SNCheck != null)
            {
                string pattern = @"^([A-Za-z0-9]+[ ]*)*[A-Za-z0-9]+$";
                Regex rex = new Regex(pattern, RegexOptions.IgnoreCase);
                Match match = rex.Match(idData);

                if (item2SNCheck.SNContentCheck == "Y" && !match.Success)
                {
                    ucMessageInfo.AddEx(this._FunctionName, this.edtInput.Value, new UserControl.Message(MessageType.Error, "ID" + idNo.ToString() + "$CS_IDContent_CheckWrong"), true);
                    return false;
                }

                if (idData.IndexOf(item2SNCheck.SNPrefix) < 0)
                {
                    ucMessageInfo.AddEx(this._FunctionName, this.edtInput.Value, new UserControl.Message(MessageType.Error, "ID" + idNo.ToString() + "$CS_Before_ID_Fletter_NotCompare $CS_RCardPrefix:" + item2SNCheck.SNPrefix), true);
                    return false;
                }

                if (idData.Trim().Length != item2SNCheck.SNLength)
                {
                    ucMessageInfo.AddEx(this._FunctionName, this.edtInput.Value, new UserControl.Message(MessageType.Error, "ID" + idNo.ToString() + "$CS_Before_ID_Length_Fletter_NotCompare $CS_ID_Length_Is:" + item2SNCheck.SNLength), true);
                    return false;
                }
            }

            return true;
        }

        #endregion

        private void checkMACID1_CheckedChanged(object sender, EventArgs e)
        {
            this.ClearVariables();
        }

        private void checkMACID2_CheckedChanged(object sender, EventArgs e)
        {
            this.ClearVariables();
        }

        private void checkMACID3_CheckedChanged(object sender, EventArgs e)
        {
            this.ClearVariables();
        }


        // 添加 “文档” 按钮的实现
        private void btnDocuments_Click(object sender, EventArgs e)
        {
           if (this.txtItemCode.Value == "")
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "", new UserControl.Message(MessageType.Normal, "$CS_ITEM_CODE_INPUT"), false);
                return;
            }

            string itemCode = this.txtItemCode.Value.Trim();
            string opCode = string.Empty;
            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
            Operation2Resource op2res = baseModelFacade.GetOperationByResource(ApplicationService.Current().ResourceCode);
            if (op2res != null)
            {
                opCode = op2res.OPCode;
            }
            //调用文档查阅窗口
            FDocView docView = new FDocView(itemCode, opCode);
            docView.ShowDialog();
        }
    }
}
