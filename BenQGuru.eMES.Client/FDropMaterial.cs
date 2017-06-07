using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;
using UserControl;

using Infragistics.Win.UltraWinGrid;


namespace BenQGuru.eMES.Client
{
    /// <summary>
    /// FMultiGoMO 的摘要说明。
    /// </summary>
    public class FDropMaterial :  BaseForm
    {

        #region 变量

        private System.ComponentModel.Container components = null;

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;

        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridLoadedPart;
        private UCLabelEdit ucLabelEditInputRcard;
        private UCLabelEdit ucLabelEditSum;
        private UCLabelEdit ucLabelEditMOCode;
        private UCMessage ucMessage1;
        private UCButton btnDrop;
        private UCButton btnExit;

        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;
        private DataTable _DataTableLoadedPart = new DataTable();
        private UltraWinGridHelper _UltraWinGridHelper1 = null;
        private string _CurrentRunningCard = string.Empty;

        private Hashtable _TSList = new Hashtable();
        private Hashtable _TSErrorCodeList = new Hashtable();

        #endregion

        #region 属性

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _DomainDataProvider;
            }
        }

        #endregion

        #region 基本

        public FDropMaterial()
        {
            UserControl.UIStyleBuilder.FormUI(this);
            InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FDropMaterial));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDrop = new UserControl.UCButton();
            this.btnExit = new UserControl.UCButton();
            this.ucLabelEditMOCode = new UserControl.UCLabelEdit();
            this.ucLabelEditSum = new UserControl.UCLabelEdit();
            this.ucLabelEditInputRcard = new UserControl.UCLabelEdit();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ultraGridLoadedPart = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ucMessage1 = new UserControl.UCMessage();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridLoadedPart)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDrop);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.ucLabelEditMOCode);
            this.panel2.Controls.Add(this.ucLabelEditSum);
            this.panel2.Controls.Add(this.ucLabelEditInputRcard);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 342);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(752, 100);
            this.panel2.TabIndex = 10;
            // 
            // btnDrop
            // 
            this.btnDrop.BackColor = System.Drawing.SystemColors.Control;
            this.btnDrop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDrop.BackgroundImage")));
            this.btnDrop.ButtonType = UserControl.ButtonTypes.Save;
            this.btnDrop.Caption = "拆解";
            this.btnDrop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDrop.Location = new System.Drawing.Point(401, 53);
            this.btnDrop.Name = "btnDrop";
            this.btnDrop.Size = new System.Drawing.Size(88, 22);
            this.btnDrop.TabIndex = 6;
            this.btnDrop.Click += new System.EventHandler(this.ucButtonSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(604, 53);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 5;
            this.btnExit.TabStop = false;
            // 
            // ucLabelEditMOCode
            // 
            this.ucLabelEditMOCode.AllowEditOnlyChecked = true;
            this.ucLabelEditMOCode.AutoSelectAll = false;
            this.ucLabelEditMOCode.AutoUpper = true;
            this.ucLabelEditMOCode.Caption = "工单";
            this.ucLabelEditMOCode.Checked = false;
            this.ucLabelEditMOCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditMOCode.Location = new System.Drawing.Point(361, 23);
            this.ucLabelEditMOCode.MaxLength = 40;
            this.ucLabelEditMOCode.Multiline = false;
            this.ucLabelEditMOCode.Name = "ucLabelEditMOCode";
            this.ucLabelEditMOCode.PasswordChar = '\0';
            this.ucLabelEditMOCode.ReadOnly = true;
            this.ucLabelEditMOCode.ShowCheckBox = false;
            this.ucLabelEditMOCode.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditMOCode.TabIndex = 3;
            this.ucLabelEditMOCode.TabNext = false;
            this.ucLabelEditMOCode.TabStop = false;
            this.ucLabelEditMOCode.Value = "";
            this.ucLabelEditMOCode.Visible = false;
            this.ucLabelEditMOCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditMOCode.XAlign = 398;
            // 
            // ucLabelEditSum
            // 
            this.ucLabelEditSum.AllowEditOnlyChecked = true;
            this.ucLabelEditSum.AutoSelectAll = false;
            this.ucLabelEditSum.AutoUpper = true;
            this.ucLabelEditSum.Caption = "已采集数量";
            this.ucLabelEditSum.Checked = false;
            this.ucLabelEditSum.EditType = UserControl.EditTypes.String;
            this.ucLabelEditSum.Location = new System.Drawing.Point(17, 23);
            this.ucLabelEditSum.MaxLength = 40;
            this.ucLabelEditSum.Multiline = false;
            this.ucLabelEditSum.Name = "ucLabelEditSum";
            this.ucLabelEditSum.PasswordChar = '\0';
            this.ucLabelEditSum.ReadOnly = true;
            this.ucLabelEditSum.ShowCheckBox = false;
            this.ucLabelEditSum.Size = new System.Drawing.Size(206, 24);
            this.ucLabelEditSum.TabIndex = 3;
            this.ucLabelEditSum.TabNext = false;
            this.ucLabelEditSum.TabStop = false;
            this.ucLabelEditSum.Value = "0";
            this.ucLabelEditSum.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditSum.XAlign = 90;
            // 
            // ucLabelEditInputRcard
            // 
            this.ucLabelEditInputRcard.AllowEditOnlyChecked = true;
            this.ucLabelEditInputRcard.AutoSelectAll = false;
            this.ucLabelEditInputRcard.AutoUpper = true;
            this.ucLabelEditInputRcard.Caption = "产品序列号";
            this.ucLabelEditInputRcard.Checked = false;
            this.ucLabelEditInputRcard.EditType = UserControl.EditTypes.String;
            this.ucLabelEditInputRcard.Location = new System.Drawing.Point(17, 53);
            this.ucLabelEditInputRcard.MaxLength = 40;
            this.ucLabelEditInputRcard.Multiline = false;
            this.ucLabelEditInputRcard.Name = "ucLabelEditInputRcard";
            this.ucLabelEditInputRcard.PasswordChar = '\0';
            this.ucLabelEditInputRcard.ReadOnly = false;
            this.ucLabelEditInputRcard.ShowCheckBox = false;
            this.ucLabelEditInputRcard.Size = new System.Drawing.Size(273, 24);
            this.ucLabelEditInputRcard.TabIndex = 0;
            this.ucLabelEditInputRcard.TabNext = false;
            this.ucLabelEditInputRcard.Value = "";
            this.ucLabelEditInputRcard.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditInputRcard.XAlign = 90;
            this.ucLabelEditInputRcard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditInput_TxtboxKeyPress);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ultraGridLoadedPart);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(752, 336);
            this.panel3.TabIndex = 11;
            // 
            // ultraGridLoadedPart
            // 
            this.ultraGridLoadedPart.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridLoadedPart.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridLoadedPart.Location = new System.Drawing.Point(8, 8);
            this.ultraGridLoadedPart.Name = "ultraGridLoadedPart";
            this.ultraGridLoadedPart.Size = new System.Drawing.Size(741, 322);
            this.ultraGridLoadedPart.TabIndex = 12;
            this.ultraGridLoadedPart.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridLoadedPart_InitializeLayout);
            this.ultraGridLoadedPart.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridLoadedPart_CellChange);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ucMessage1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 336);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(752, 6);
            this.panel4.TabIndex = 12;
            // 
            // ucMessage1
            // 
            this.ucMessage1.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessage1.ButtonVisible = false;
            this.ucMessage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessage1.Location = new System.Drawing.Point(0, 0);
            this.ucMessage1.Name = "ucMessage1";
            this.ucMessage1.Size = new System.Drawing.Size(752, 6);
            this.ucMessage1.TabIndex = 3;
            this.ucMessage1.TabStop = false;
            // 
            // FDropMaterial
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(752, 442);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "FDropMaterial";
            this.Text = "拆解处理";
            this.Load += new System.EventHandler(this.FDropMaterial_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FDropMaterial_FormClosed);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridLoadedPart)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        #region 事件

        private void FDropMaterial_Load(object sender, EventArgs e)
        {
            InitializeUltraGrid();
            InitializeVariables();
            ClearHashtables();
            //this.InitPageLanguage();
        }

        private void FDropMaterial_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DataProvider != null)
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
            }
        }

        private void ucLabelEditInput_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                _CurrentRunningCard = Web.Helper.FormatHelper.CleanString(ucLabelEditInputRcard.Value.Trim().ToUpper());

                Messages msg = LoadData(_CurrentRunningCard);
                ucMessage1.Add(msg);
                if (!msg.IsSuccess())
                {
                    InitializeVariables();
                }

                ClearHashtables();
            }
        }

        private void ultraGridLoadedPart_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            _UltraWinGridHelper1 = new UltraWinGridHelper(this.ultraGridLoadedPart);

            _UltraWinGridHelper1.AddCheckColumn("Check", "");
            _UltraWinGridHelper1.AddCheckColumn("TSCheck", "物料维修");
            _UltraWinGridHelper1.AddReadOnlyColumn("ErrorCode", "不良代码");
            _UltraWinGridHelper1.AddReadOnlyColumn("Index", "顺序号");
            _UltraWinGridHelper1.AddReadOnlyColumn("ItemCode", "物料代码");
            _UltraWinGridHelper1.AddReadOnlyColumn("MOCode", "工单代码");
            _UltraWinGridHelper1.AddReadOnlyColumn("PackedNo", "最小包装号");
            _UltraWinGridHelper1.AddReadOnlyColumn("ItemDesc", "物料描述");
            _UltraWinGridHelper1.AddCommonColumn("NewBarcode", "新条码");
            _UltraWinGridHelper1.AddReadOnlyColumn("MCardType", "MCardType");

            //this.InitGridLanguage(ultraGridLoadedPart);
        }

        private void ultraGridLoadedPart_CellChange(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Key == "Check" && !bool.Parse(e.Cell.Text))
            {
                ultraGridLoadedPart.Rows[e.Cell.Row.Index].Cells["TSCheck"].Value = false;
                ultraGridLoadedPart.Rows[e.Cell.Row.Index].Cells["ErrorCode"].Value = string.Empty;
                ultraGridLoadedPart.Rows[e.Cell.Row.Index].Cells["NewBarcode"].Value = string.Empty;

                RemoveHashtableItem(e.Cell.Row.Index);
            }

            if (e.Cell.Column.Key == "TSCheck")
            {
                if (ultraGridLoadedPart.Rows[e.Cell.Row.Index].Cells["MCardType"].Text == MCardType.MCardType_Keyparts)
                {
                    if (bool.Parse(e.Cell.Text))
                    {
                        Domain.TS.TS newTS = GetNewTS(
                            _CurrentRunningCard,
                            ultraGridLoadedPart.Rows[e.Cell.Row.Index].Cells["ItemCode"].Text,
                            ultraGridLoadedPart.Rows[e.Cell.Row.Index].Cells["PackedNo"].Text,
                            ApplicationService.Current().UserCode);                        

                        FTSErrorEdit form = new FTSErrorEdit();
                        form.RunningCardTitle = "Material";
                        form.AddTSErrorCodeWhenSave = false;
                        form.CurrentTS = newTS;
                        form.ShowDialog();

                        TSErrorCode[] selectedTSErrorCodeList = form.SelectedTSErrorCode;
                        if (selectedTSErrorCodeList == null)
                        {
                            e.Cell.Value = false;
                        }
                        else
                        {
                            string codeList = string.Empty;
                            for (int i = 0; i < selectedTSErrorCodeList.Length; i++)
                            {
                                if (codeList.Trim().Length > 0)
                                {
                                    codeList += "; ";
                                }
                                codeList += selectedTSErrorCodeList[i].ErrorCodeGroup + ":" + selectedTSErrorCodeList[i].ErrorCode;
                            }

                            ultraGridLoadedPart.Rows[e.Cell.Row.Index].Cells["ErrorCode"].Value = codeList;

                            _TSList.Add(e.Cell.Row.Index, newTS);
                            _TSErrorCodeList.Add(e.Cell.Row.Index, selectedTSErrorCodeList);

                            ultraGridLoadedPart.Rows[e.Cell.Row.Index].Cells["Check"].Value = true;
                        }
                    }
                    else
                    {
                        ultraGridLoadedPart.Rows[e.Cell.Row.Index].Cells["ErrorCode"].Value = string.Empty;
                        RemoveHashtableItem(e.Cell.Row.Index);
                    }
                }
                else
                {
                    e.Cell.Value = false;
                }
            }

            if (e.Cell.Column.Key == "NewBarcode" && e.Cell.Text.Trim().Length > 0 && !bool.Parse(ultraGridLoadedPart.Rows[e.Cell.Row.Index].Cells["Check"].Text))
            {
                ultraGridLoadedPart.Rows[e.Cell.Row.Index].Cells["Check"].Value = true;
            }

            if (e.Cell.Column.Key == "NewBarcode" && e.Cell.Text.Trim().Length <= 0 && bool.Parse(ultraGridLoadedPart.Rows[e.Cell.Row.Index].Cells["Check"].Text))
            {
                ultraGridLoadedPart.Rows[e.Cell.Row.Index].Cells["Check"].Value = false;
            }
        }

        private void ucButtonSave_Click(object sender, EventArgs e)
        {

            if (_CurrentRunningCard == string.Empty)
            {
                return;
            }

            Messages msg = DropLoadedParts(_CurrentRunningCard);
            ucMessage1.Add(msg);

            if (msg.IsSuccess())
            {
                this.ucLabelEditSum.Value = Convert.ToString(Convert.ToInt32(this.ucLabelEditSum.Value) + 1);
                ucMessage1.Add(new UserControl.Message(MessageType.Success, "$CS_KEYPARTS_DROPMATERIAL_SUCCESS"));

                InitializeVariables();
                ClearHashtables();
            }
        }

        #endregion

        #region 函数

        private void InitUltraGridUI(UltraGrid ultraGrid)
        {
            ultraGrid.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGrid.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGrid.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGrid.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGrid.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGrid.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGrid.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGrid.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGrid.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void InitializeVariables()
        {
            _CurrentRunningCard = string.Empty;
            _DataTableLoadedPart.Clear();            

            ucLabelEditInputRcard.Value = string.Empty;
            ucLabelEditMOCode.Value = string.Empty;

            ucMessage1.Add(">>$CS_Please_Input_RunningCard ");
            this.ucLabelEditInputRcard.TextFocus(false, true);
        }

        private void ClearHashtables()
        {
            _TSList.Clear();
            _TSErrorCodeList.Clear();
        }

        private void RemoveHashtableItem(int index)
        {
            if (_TSList[index] != null)
            {
                _TSList.Remove(index);
            }

            if (_TSErrorCodeList[index] != null)
            {
                _TSErrorCodeList.Remove(index);
            }
        }

        private void InitializeUltraGrid()
        {
            InitUltraGridUI(ultraGridLoadedPart);

            _DataTableLoadedPart.Columns.Add("Check", typeof(bool));
            _DataTableLoadedPart.Columns.Add("TSCheck", typeof(bool));
            _DataTableLoadedPart.Columns.Add("ErrorCode", typeof(string));
            _DataTableLoadedPart.Columns.Add("Index", typeof(string));
            _DataTableLoadedPart.Columns.Add("ItemCode", typeof(string));
            _DataTableLoadedPart.Columns.Add("MOCode", typeof(string));
            _DataTableLoadedPart.Columns.Add("PackedNo", typeof(string));
            _DataTableLoadedPart.Columns.Add("ItemDesc", typeof(string));
            _DataTableLoadedPart.Columns.Add("NewBarcode", typeof(string));
            _DataTableLoadedPart.Columns.Add("MCardType", typeof(string));


            _DataTableLoadedPart.Columns["Check"].ReadOnly = false;
            _DataTableLoadedPart.Columns["TSCheck"].ReadOnly = false;
            _DataTableLoadedPart.Columns["ErrorCode"].ReadOnly = false;
            _DataTableLoadedPart.Columns["Index"].ReadOnly = true;
            _DataTableLoadedPart.Columns["ItemCode"].ReadOnly = true;
            _DataTableLoadedPart.Columns["MOCode"].ReadOnly = true;
            _DataTableLoadedPart.Columns["PackedNo"].ReadOnly = true;
            _DataTableLoadedPart.Columns["ItemDesc"].ReadOnly = true;
            _DataTableLoadedPart.Columns["NewBarcode"].ReadOnly = false;
            _DataTableLoadedPart.Columns["MCardType"].ReadOnly = true;

            this.ultraGridLoadedPart.DataSource = this._DataTableLoadedPart;

            _DataTableLoadedPart.Clear();

            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["Check"].Width = 16;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["TSCheck"].Width = 60;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["ErrorCode"].Width = 100;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["Index"].Width = 40;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["ItemCode"].Width = 60;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["MOCode"].Width = 80;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["PackedNo"].Width = 120;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["ItemDesc"].Width = 120;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["NewBarcode"].Width = 120;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["MCardType"].Width = 120;

            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["Check"].CellActivation = Activation.AllowEdit;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["TSCheck"].CellActivation = Activation.AllowEdit;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["ErrorCode"].CellActivation = Activation.NoEdit;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["Index"].CellActivation = Activation.NoEdit;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["ItemCode"].CellActivation = Activation.NoEdit;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["MOCode"].CellActivation = Activation.NoEdit;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["PackedNo"].CellActivation = Activation.NoEdit;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["ItemDesc"].CellActivation = Activation.NoEdit;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["NewBarcode"].CellActivation = Activation.AllowEdit;
            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["MCardType"].CellActivation = Activation.NoEdit;

            ((Infragistics.Win.CheckEditor)ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["TSCheck"].Editor).CheckAlign = ContentAlignment.MiddleCenter;

            ultraGridLoadedPart.DisplayLayout.Bands[0].Columns["MCardType"].Hidden = true;
        }

        private ProductInfo GetProduct(string runningCard)
        {
            ProductInfo returnValue = null;

            DataCollect.ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
            Messages msg = onLine.GetIDInfo(runningCard);

            if (msg.IsSuccess())
            {
                returnValue = (ProductInfo)msg.GetData().Values[0];
            }

            return returnValue;
        }

        private Messages LoadData(string rcard)
        {
            Messages msg = new Messages();

            _DataTableLoadedPart.Clear();

            MOFacade moFacade = new MOFacade(this.DataProvider);
            OPBOMFacade opBOMFacade = new OPBOMFacade(this.DataProvider);
            MaterialFacade materialFacade = new MaterialFacade(this.DataProvider);
            ItemFacade itemFacade = new ItemFacade(this.DataProvider);

            //根据当前的序列号获取产品原始的序列号
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(rcard, string.Empty);

            //Get Product Info
            ProductInfo product = GetProduct(sourceRCard);

            if (product == null || product.LastSimulation == null)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$NoSimulation"));
                return msg;
            }

            // Marked By HI1/Venus.Feng on 20081013 for Hisense Version : GOOD can do drop
            /*
            if (product.LastSimulation.ProductStatus != ProductStatus.NG)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$DropMaterial_Need_NG"));
                return msg;
            }
            */
            // End Marked

            //Get loaded parts
            OnWIPItem[] onWIPItems = materialFacade.QueryLoadedPartByRCard(product.LastSimulation.RunningCard, string.Empty);

            if (onWIPItems == null)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$DropMaterial_Need_CINNO"));
                return msg;
            }

            try
            {
                if (onWIPItems != null)
                {
                    for (int i = 0; i < onWIPItems.Length; i++)
                    {
                        MO mo = (MO)moFacade.GetMO(onWIPItems[i].MOCode);
                        int orgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                        if (mo != null)
                        {
                            orgID = mo.OrganizationID;
                        }

                        Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterial(onWIPItems[i].MItemCode, orgID);
                        //changed by hiro 08/11/04
                        if (material != null)
                        {
                            _DataTableLoadedPart.Rows.Add(new object[]{
                                false,
                                false,
                                string.Empty,
                                i + 1,
                                onWIPItems[i].MItemCode,
                                onWIPItems[i].MOCode,
                                onWIPItems[i].MCARD,
                                material.MaterialDescription == null ? "" : material.MaterialDescription,
                                string.Empty,
                                onWIPItems[i].MCardType});
                        }
                        //end by hiro
                    }
                }
            }
            catch (Exception E)
            {
                msg.Add(new UserControl.Message(E));
            }

            return msg;
        }

        private Messages DropLoadedParts(string rcard)
        {
            Messages msg = new Messages();

            ActionOnLineHelper onLine = new ActionOnLineHelper(DataProvider);
            MaterialFacade materialFacade = new MaterialFacade(this.DataProvider);
            ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            TSFacade tsFacade = new TSFacade(this.DataProvider);

            string sourceRCard = dataCollectFacade.GetSourceCard(rcard.Trim().ToUpper(), string.Empty);

            ProductInfo product = GetProduct(sourceRCard);

            if (product == null || product.LastSimulation == null)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$NoSimulation"));
                return msg;
            }

            // Marked By HI1/Venus.Feng on 20081013 for Hisense Version : GOOD can do drop
            /*
            if (product.LastSimulation.ProductStatus != ProductStatus.NG)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$DropMaterial_Need_NG"));
                return msg;
            }
            */
            // End Marked

            msg.Add(new UserControl.Message(rcard));

            //获取所有需要拆解或者替换的Parts
            ArrayList partsToUnload = new ArrayList();

            for (int i = 0; i < this.ultraGridLoadedPart.Rows.Count; i++)
            {
                if (ultraGridLoadedPart.Rows[i].Cells[0].Text.ToLower() == "true")
                {
                    InnoObject innoObject = new InnoObject();

                    innoObject.LineIndex = i;
                    innoObject.MCard = ultraGridLoadedPart.Rows[i].Cells["PackedNo"].Text;
                    innoObject.MCardType = ultraGridLoadedPart.Rows[i].Cells["MCardType"].Text;
                    innoObject.ItemIndex = partsToUnload.Count + 1;
                    innoObject.MItemCode = ultraGridLoadedPart.Rows[i].Cells["ItemCode"].Text;
                    innoObject.MOCode = ultraGridLoadedPart.Rows[i].Cells["MOCode"].Text;
                    innoObject.Qty = 1;
                    innoObject.NewBarcode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ultraGridLoadedPart.Rows[i].Cells["NewBarcode"].Text.Trim()));

                    partsToUnload.Add(innoObject);
                }
            }

            //对于需要替换的Parts，需要做类似上料中的解析和检查
            for (int i = 0; i < partsToUnload.Count; i++)
            {
                InnoObject innoObject = (InnoObject)partsToUnload[i];

                if (innoObject.NewBarcode.Trim().Length > 0)
                {
                    //抓取物料的设定
                    Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterial(innoObject.MItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                    if (material == null)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$Error_Material_NotFound"));
                        return msg;
                    }

                    string barcode = innoObject.NewBarcode.Trim();

                    //模拟一个OPBOMDetail
                    OPBOMDetail detailTemp = new OPBOMDetail();
                    detailTemp.OPBOMItemControlType = material.MaterialControlType;
                    detailTemp.OPBOMParseType = material.MaterialParseType;
                    detailTemp.OPBOMCheckType = material.MaterialCheckType;
                    detailTemp.CheckStatus = material.CheckStatus;
                    detailTemp.SerialNoLength = material.SerialNoLength;
                    detailTemp.NeedVendor = material.NeedVendor;
                    detailTemp.OPBOMSourceItemCode = innoObject.MItemCode;
                    detailTemp.OPBOMItemCode = innoObject.MItemCode;
                    detailTemp.OPBOMItemQty = 1;


                    MINNO newMINNO = new MINNO();
                    newMINNO.MOCode = innoObject.MOCode.Trim();
                    newMINNO.MItemCode = material.MaterialCode.Trim();

                    Messages collectMessage = dataCollectFacade.GetMINNOByBarcode(detailTemp, barcode, newMINNO.MOCode, null, false,false, out newMINNO);

                    if (collectMessage.IsSuccess())
                    {
                        innoObject.NewLotNo = newMINNO.LotNO;
                        innoObject.NewPCBA = newMINNO.PCBA;
                        innoObject.NewBIOS = newMINNO.BIOS;
                        innoObject.NewVersion = newMINNO.Version;
                        innoObject.NewVendorItemCode = newMINNO.VendorItemCode;
                        innoObject.NewVendorCode = newMINNO.VendorCode;
                        innoObject.NewDateCode = newMINNO.DateCode;
                    }
                    else
                    {
                        msg.AddMessages(collectMessage);
                        return msg;
                    }
                }
            }

            try
            {
                this.DataProvider.BeginTransaction();

                //更新试流单
                //下料中处理tbltry，tbltry2rcard
                for (int i = 0; i < partsToUnload.Count; i++)
                {
                    InnoObject innoObject = (InnoObject)partsToUnload[i];

                    TryEventArgs tryEventArgs = new TryEventArgs(
                        ActionType.DataCollectAction_TryNew, ApplicationService.Current().UserCode, product.LastSimulation.OPCode, ApplicationService.Current().ResourceCode,
                        product.LastSimulation.ItemCode, sourceRCard, innoObject.MItemCode, innoObject.MCard, string.Empty, false, false);

                    msg.AddMessages(onLine.ActionWithTransaction(tryEventArgs));
                    if (!msg.IsSuccess())
                    {
                        DataProvider.RollbackTransaction();
                        return msg;
                    }
                }

                //更新试流单
                //上料中处理tbltry，tbltry2rcard
                for (int i = 0; i < partsToUnload.Count; i++)
                {
                    InnoObject innoObject = (InnoObject)partsToUnload[i];
                    if (innoObject.NewBarcode.Trim().Length > 0)
                    {
                        TryEventArgs tryEventArgs = new TryEventArgs(
                        ActionType.DataCollectAction_TryNew, ApplicationService.Current().UserCode, product.LastSimulation.OPCode, ApplicationService.Current().ResourceCode,
                        product.LastSimulation.ItemCode, sourceRCard, innoObject.MItemCode, innoObject.NewBarcode, string.Empty, true, true);

                        msg.AddMessages(onLine.ActionWithTransaction(tryEventArgs));
                        if (!msg.IsSuccess())
                        {
                            DataProvider.RollbackTransaction();
                            return msg;
                        }
                    }
                }

                //检查新上料是否在TS中而不可用
                for (int i = 0; i < partsToUnload.Count; i++)
                {
                    InnoObject innoObject = (InnoObject)partsToUnload[i];
                    if (innoObject.NewBarcode.Trim().Length > 0)
                    {
                        if (!tsFacade.RunningCardCanBeClollected(innoObject.NewBarcode.Trim(), CardType.CardType_Part))
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$Error_Material_InTSOrScrapped $SERIAL_NO=" + innoObject.NewBarcode.Trim()));
                            DataProvider.RollbackTransaction();
                            return msg;
                        }
                    }
                }

                //拆解或者替换处理tblonwipitem，tblsimulationreport
                DropMaterialEventArgs dropMaterialEventArgs = new DropMaterialEventArgs(ActionType.DataCollectAction_DropMaterial, product.LastSimulation.RunningCard,
                    ApplicationService.Current().UserCode, ApplicationService.Current().ResourceCode,
                    product);
                dropMaterialEventArgs.OnwipItems = partsToUnload.ToArray();

                msg.AddMessages(onLine.ActionWithTransaction(dropMaterialEventArgs));
                if (!msg.IsSuccess())
                {
                    DataProvider.RollbackTransaction();
                    return msg;
                }

                //针对拆解下的物料，做TS相关的动作
                for (int i = 0; i < partsToUnload.Count; i++)
                {
                    InnoObject innoObject = (InnoObject)partsToUnload[i];

                    if (_TSList[innoObject.LineIndex] != null)
                    {
                        if (_TSErrorCodeList[innoObject.LineIndex] == null)
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$CS_Please_Select_ErrorCode"));
                            DataProvider.RollbackTransaction();
                            return msg;
                        }

                        Domain.TS.TS oldTS = (Domain.TS.TS)tsFacade.QueryLastTSByRunningCard(innoObject.MCard);
                        if (oldTS != null)
                        {
                            if (oldTS.TSStatus == TSStatus.TSStatus_New
                                || oldTS.TSStatus == TSStatus.TSStatus_Confirm
                                || oldTS.TSStatus == TSStatus.TSStatus_TS)
                            {
                                msg.Add(new UserControl.Message(MessageType.Error, "$Error_Material_InTS"));
                                DataProvider.RollbackTransaction();
                                return msg;
                            }
                            else if (oldTS.TSStatus != TSStatus.TSStatus_Reflow
                                && oldTS.TSStatus != TSStatus.TSStatus_Split
                                && oldTS.TSStatus != TSStatus.TSStatus_Complete
                                && oldTS.TSStatus != TSStatus.TSStatus_Scrap)
                            {
                                msg.Add(new UserControl.Message(MessageType.Error, "$Error_Material_WrongTSStatus"));
                                DataProvider.RollbackTransaction();
                                return msg;
                            }
                        }

                        Domain.TS.TS newTS = (Domain.TS.TS)_TSList[innoObject.LineIndex];
                        tsFacade.AddTS(newTS);
                        foreach (TSErrorCode tsErrorCode in (TSErrorCode[])_TSErrorCodeList[innoObject.LineIndex])
                        {
                            tsErrorCode.RunningCard = newTS.RunningCard;
                            tsErrorCode.RunningCardSequence = newTS.RunningCardSequence;
                            tsErrorCode.ItemCode = newTS.ItemCode;
                            tsErrorCode.ModelCode = newTS.ModelCode;
                            tsErrorCode.MOCode = newTS.MOCode;
                            tsErrorCode.MOSeq = newTS.MOSeq;
                            tsErrorCode.MaintainUser = newTS.MaintainUser;
                            tsErrorCode.MaintainDate = newTS.MaintainDate;
                            tsErrorCode.MaintainTime = newTS.MaintainTime;

                            tsFacade.AddTSErrorCode(tsErrorCode);
                        }
                    }
                }

                if (!msg.IsSuccess())
                {
                    DataProvider.RollbackTransaction();
                    return msg;
                }

                if (msg.IsSuccess())
                {
                    DataProvider.CommitTransaction();
                }
                else
                {
                    DataProvider.RollbackTransaction();
                }
            }
            catch (Exception e)
            {
                this.DataProvider.RollbackTransaction();
                msg.Add(new UserControl.Message(e));
            }

            return msg;
        }

        #endregion

        private Domain.TS.TS GetNewTS(string runningCard, string partItemCode, string partRunningCard, string userCode)
        {
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            ShiftModelFacade shiftModelFacade = new ShiftModelFacade(this.DataProvider);
            ModelFacade modelFacade = new ModelFacade(this.DataProvider);
            TSFacade tsFacade = new TSFacade(this.DataProvider);
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);

            string sourceRCard = dataCollectFacade.GetSourceCard(runningCard.Trim().ToUpper(), string.Empty);

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            SimulationReport lastSimulation = dataCollectFacade.GetLastSimulationReport(sourceRCard);

            if (lastSimulation == null)
            {
                return null;
            }

            Domain.TS.TS newTS = tsFacade.CreateNewTS();

            newTS.TSId = Guid.NewGuid().ToString();
            newTS.RunningCard = partRunningCard;
            newTS.RunningCardSequence = dataCollectFacade.GetMaxRCardSequenceFromTS(partRunningCard) + 100;
            newTS.TranslateCard = partRunningCard;
            newTS.TranslateCardSequence = newTS.RunningCardSequence;
            newTS.SourceCard = partRunningCard;
            newTS.SourceCardSequence = newTS.RunningCardSequence;
            newTS.CardType = CardType.CardType_Part;
            newTS.ReplacedRunningCard = " ";

            newTS.ItemCode = partItemCode;
            Model model = (Model)modelFacade.GetModelByItemCode(partItemCode);
            if (model == null)
            {
                Parameter parameter = (Parameter)systemSettingFacade.GetParameter("PING", "DEFAULT_MODEL_CODE");
                if (parameter != null)
                {
                    newTS.ModelCode = parameter.ParameterAlias.Trim().ToUpper();
                }
            }
            else
            {
                newTS.ModelCode = model.ModelCode;
            }

            newTS.MOCode = lastSimulation.MOCode;
            newTS.FromRouteCode = lastSimulation.RouteCode;
            newTS.FromOPCode = lastSimulation.OPCode;
            newTS.FromResourceCode = lastSimulation.ResourceCode;
            newTS.FromSegmentCode = lastSimulation.SegmentCode;
            newTS.FromStepSequenceCode = lastSimulation.StepSequenceCode;
            newTS.FromShiftTypeCode = lastSimulation.ShiftTypeCode;
            newTS.MOSeq = lastSimulation.MOSeq;

            TimePeriod tp = (TimePeriod)shiftModelFacade.GetTimePeriod(newTS.FromShiftTypeCode, dbDateTime.DBTime);
            if (tp != null)
            {
                newTS.FromTimePeriodCode = tp.TimePeriodCode;
                newTS.FromShiftCode = tp.ShiftCode;
                newTS.FromShiftDay = shiftModelFacade.GetShiftDay(tp, dbDateTime.DateTime);
            }

            newTS.FromUser = userCode;
            newTS.FromDate = dbDateTime.DBDate;
            newTS.FormTime = dbDateTime.DBTime;

            newTS.MaintainUser = userCode;
            newTS.MaintainDate = dbDateTime.DBDate;
            newTS.MaintainTime = dbDateTime.DBTime;

            newTS.TSTimes = tsFacade.GetMaxTSTimes(partRunningCard) + 1;
            newTS.FromInputType = TSSource.TSSource_TS;
            newTS.TSStatus = TSStatus.TSStatus_New;
            newTS.TransactionStatus = TransactionStatus.TransactionStatus_NO;

            return newTS;
        }

    }
}
