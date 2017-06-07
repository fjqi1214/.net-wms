using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Web.Helper;
using UserControl;

/*
 * Laws Lu，2006/09/15	用户提出FIFO，允许按照生产批号来管控物料
 * 1,集成备料依然By工单备料到资源
 * 2,工单对应产品的工序BOM必须已经生效
 * 3,备料资料来源于该工单的发料资料，因此用户输入的Lot号必须存在于该工单的发料资料中，且包含该Lot号的发料记录SUM(QTY）必须<0.实现该逻辑时需要考虑性能问题。发料资料中相同Lot号只对应一种物料代码
 * 3,用户扫描输入物料Lot号之后回车带出物料代码，并通过批号解析出DateCode。解析规则是批号的3~8码对应DateCode，DateCode格式是年（4位）月（2位）日（2位）。其他信息由用户手工输入，且非必输
 * 4,集成备料与工序BOM的关联检查逻辑保持不变
 *
 */
namespace BenQGuru.eMES.Client
{
	public class FINNO : Form
	{
		private System.Data.DataSet dataSet1;
		private System.Data.DataTable dtMINNO;
		private System.Data.DataColumn dataColumn1;
		private System.Data.DataColumn dataColumn2;
		private System.Data.DataColumn dataColumn3;
		private System.Data.DataColumn dataColumn4;
		private System.Data.DataColumn dataColumn5;
		private System.Data.DataColumn dataColumn6;
		private System.Data.DataColumn dataColumn7;
		private System.Data.DataColumn dataColumn8;
		private System.Data.DataColumn dataColumn9;
		private System.Data.DataColumn dataColumn10;
		private System.Data.DataColumn dataColumn11;
		private System.Data.DataColumn dataColumn12;
		
		private System.Windows.Forms.GroupBox grpQuery;
		private System.Windows.Forms.Panel panelButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private UserControl.UCLabelEdit ucLEMO;
		private UserControl.UCLabelCombox ucLCRoute;
		private UserControl.UCLabelCombox ucLCOp;
		private UserControl.UCLabelCombox ucLCResource;
		private UserControl.UCLabelCombox ucLCToRes;
		private UserControl.UCLabelEdit ucLEItemCode;
		private UserControl.UCLabelEdit ucLEBIOS;
		private UserControl.UCLabelEdit ucLEPCBA;
		private UserControl.UCLabelEdit ucLEVendorItem;
		private UserControl.UCLabelEdit ucLEDateCode;
		private UserControl.UCLabelEdit ucLETryItemCode;
		private UserControl.UCLabelEdit ucLEVersion;
		private UserControl.UCLabelEdit ucLEVendorCode;
		private UserControl.UCLabelEdit ucLELotNo;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridINNO;
		private UserControl.UCButton ucBtnDelete;
		private UserControl.UCButton ucBtnExit;
		private UserControl.UCButton ucBtnSave;
		private UserControl.UCButton ucBtnUpdate;
		private UserControl.UCButton ucBtnAdd;
		private UserControl.UCButton ucBtnCopy;
		private System.ComponentModel.IContainer components = null;
		
		private MaterialFacade _facade = null;
		private Infragistics.Win.UltraWinGrid.UltraGridRow _emptyRow = null;
		private UserControl.UCButton ucBtnDeleteINNO;
		private System.Data.DataColumn dataColumn13;
		private System.Windows.Forms.Label lblMitemName;
		private System.Windows.Forms.CheckBox chkCheckERPBOM;
		private UserControl.UCLabelEdit txtBarcode;

		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		public FINNO()
		{
			// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化
			
			UserControl.UIStyleBuilder.GridUI(ultraGridINNO);
			UserControl.UIStyleBuilder.FormUI(this);
			
			this._facade = new MaterialFacade( this.DataProvider );
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FINNO));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Table1", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column2");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column3");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column4");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column5");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column6");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column7");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column8");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column9");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column10");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column11");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column12");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column13");
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.dtMINNO = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn10 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataColumn12 = new System.Data.DataColumn();
            this.dataColumn13 = new System.Data.DataColumn();
            this.dataSet1 = new System.Data.DataSet();
            this.grpQuery = new System.Windows.Forms.GroupBox();
            this.ucLEMO = new UserControl.UCLabelEdit();
            this.ucBtnCopy = new UserControl.UCButton();
            this.ucLCToRes = new UserControl.UCLabelCombox();
            this.ucLCOp = new UserControl.UCLabelCombox();
            this.ucLCRoute = new UserControl.UCLabelCombox();
            this.ucLCResource = new UserControl.UCLabelCombox();
            this.panelButton = new System.Windows.Forms.Panel();
            this.ucBtnDeleteINNO = new UserControl.UCButton();
            this.ucBtnDelete = new UserControl.UCButton();
            this.ucBtnExit = new UserControl.UCButton();
            this.ucBtnSave = new UserControl.UCButton();
            this.ucBtnUpdate = new UserControl.UCButton();
            this.ucBtnAdd = new UserControl.UCButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBarcode = new UserControl.UCLabelEdit();
            this.chkCheckERPBOM = new System.Windows.Forms.CheckBox();
            this.lblMitemName = new System.Windows.Forms.Label();
            this.ucLEItemCode = new UserControl.UCLabelEdit();
            this.ucLEBIOS = new UserControl.UCLabelEdit();
            this.ucLEPCBA = new UserControl.UCLabelEdit();
            this.ucLEVendorItem = new UserControl.UCLabelEdit();
            this.ucLEDateCode = new UserControl.UCLabelEdit();
            this.ucLETryItemCode = new UserControl.UCLabelEdit();
            this.ucLEVersion = new UserControl.UCLabelEdit();
            this.ucLEVendorCode = new UserControl.UCLabelEdit();
            this.ucLELotNo = new UserControl.UCLabelEdit();
            this.ultraGridINNO = new Infragistics.Win.UltraWinGrid.UltraGrid();
            ((System.ComponentModel.ISupportInitialize)(this.dtMINNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.grpQuery.SuspendLayout();
            this.panelButton.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridINNO)).BeginInit();
            this.SuspendLayout();
            // 
            // dtMINNO
            // 
            this.dtMINNO.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn11,
            this.dataColumn12,
            this.dataColumn13});
            this.dtMINNO.TableName = "Table1";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "Column1";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "Column2";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "Column3";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "Column4";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "Column5";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "Column6";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "Column7";
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "Column8";
            // 
            // dataColumn9
            // 
            this.dataColumn9.ColumnName = "Column9";
            // 
            // dataColumn10
            // 
            this.dataColumn10.ColumnName = "Column10";
            // 
            // dataColumn11
            // 
            this.dataColumn11.ColumnName = "Column11";
            // 
            // dataColumn12
            // 
            this.dataColumn12.ColumnName = "Column12";
            // 
            // dataColumn13
            // 
            this.dataColumn13.ColumnName = "Column13";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Locale = new System.Globalization.CultureInfo("zh-CN");
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dtMINNO});
            // 
            // grpQuery
            // 
            this.grpQuery.BackColor = System.Drawing.Color.Gainsboro;
            this.grpQuery.Controls.Add(this.ucLEMO);
            this.grpQuery.Controls.Add(this.ucBtnCopy);
            this.grpQuery.Controls.Add(this.ucLCToRes);
            this.grpQuery.Controls.Add(this.ucLCOp);
            this.grpQuery.Controls.Add(this.ucLCRoute);
            this.grpQuery.Controls.Add(this.ucLCResource);
            this.grpQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpQuery.Location = new System.Drawing.Point(0, 0);
            this.grpQuery.Name = "grpQuery";
            this.grpQuery.Size = new System.Drawing.Size(760, 80);
            this.grpQuery.TabIndex = 0;
            this.grpQuery.TabStop = false;
            // 
            // ucLEMO
            // 
            this.ucLEMO.AllowEditOnlyChecked = true;
            this.ucLEMO.Caption = "工单";
            this.ucLEMO.Checked = false;
            this.ucLEMO.EditType = UserControl.EditTypes.String;
            this.ucLEMO.Location = new System.Drawing.Point(16, 16);
            this.ucLEMO.MaxLength = 40;
            this.ucLEMO.Multiline = false;
            this.ucLEMO.Name = "ucLEMO";
            this.ucLEMO.PasswordChar = '\0';
            this.ucLEMO.ReadOnly = false;
            this.ucLEMO.ShowCheckBox = false;
            this.ucLEMO.Size = new System.Drawing.Size(170, 24);
            this.ucLEMO.TabIndex = 0;
            this.ucLEMO.TabNext = true;
            this.ucLEMO.Value = "";
            this.ucLEMO.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEMO.XAlign = 53;
            this.ucLEMO.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEMO_TxtboxKeyPress);
            // 
            // ucBtnCopy
            // 
            this.ucBtnCopy.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnCopy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnCopy.BackgroundImage")));
            this.ucBtnCopy.ButtonType = UserControl.ButtonTypes.Copy;
            this.ucBtnCopy.Caption = "复制";
            this.ucBtnCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnCopy.Location = new System.Drawing.Point(232, 48);
            this.ucBtnCopy.Name = "ucBtnCopy";
            this.ucBtnCopy.Size = new System.Drawing.Size(88, 22);
            this.ucBtnCopy.TabIndex = 5;
            this.ucBtnCopy.Click += new System.EventHandler(this.ucBtnCopy_Click);
            // 
            // ucLCToRes
            // 
            this.ucLCToRes.AllowEditOnlyChecked = true;
            this.ucLCToRes.Caption = "复制到资源";
            this.ucLCToRes.Checked = false;
            this.ucLCToRes.Location = new System.Drawing.Point(17, 48);
            this.ucLCToRes.Name = "ucLCToRes";
            this.ucLCToRes.SelectedIndex = -1;
            this.ucLCToRes.ShowCheckBox = false;
            this.ucLCToRes.Size = new System.Drawing.Size(206, 24);
            this.ucLCToRes.TabIndex = 4;
            this.ucLCToRes.WidthType = UserControl.WidthTypes.Normal;
            this.ucLCToRes.XAlign = 90;
            // 
            // ucLCOp
            // 
            this.ucLCOp.AllowEditOnlyChecked = true;
            this.ucLCOp.Caption = "工序";
            this.ucLCOp.Checked = false;
            this.ucLCOp.Location = new System.Drawing.Point(384, 16);
            this.ucLCOp.Name = "ucLCOp";
            this.ucLCOp.SelectedIndex = -1;
            this.ucLCOp.ShowCheckBox = false;
            this.ucLCOp.Size = new System.Drawing.Size(170, 24);
            this.ucLCOp.TabIndex = 2;
            this.ucLCOp.WidthType = UserControl.WidthTypes.Normal;
            this.ucLCOp.XAlign = 421;
            this.ucLCOp.SelectedIndexChanged += new System.EventHandler(this.ucLCOp_SelectedIndexChanged);
            // 
            // ucLCRoute
            // 
            this.ucLCRoute.AllowEditOnlyChecked = true;
            this.ucLCRoute.Caption = "途程";
            this.ucLCRoute.Checked = false;
            this.ucLCRoute.Location = new System.Drawing.Point(200, 16);
            this.ucLCRoute.Name = "ucLCRoute";
            this.ucLCRoute.SelectedIndex = -1;
            this.ucLCRoute.ShowCheckBox = false;
            this.ucLCRoute.Size = new System.Drawing.Size(170, 24);
            this.ucLCRoute.TabIndex = 1;
            this.ucLCRoute.WidthType = UserControl.WidthTypes.Normal;
            this.ucLCRoute.XAlign = 237;
            this.ucLCRoute.SelectedIndexChanged += new System.EventHandler(this.ucLCRoute_SelectedIndexChanged);
            // 
            // ucLCResource
            // 
            this.ucLCResource.AllowEditOnlyChecked = true;
            this.ucLCResource.Caption = "资源";
            this.ucLCResource.Checked = false;
            this.ucLCResource.Location = new System.Drawing.Point(568, 16);
            this.ucLCResource.Name = "ucLCResource";
            this.ucLCResource.SelectedIndex = -1;
            this.ucLCResource.ShowCheckBox = false;
            this.ucLCResource.Size = new System.Drawing.Size(170, 24);
            this.ucLCResource.TabIndex = 3;
            this.ucLCResource.WidthType = UserControl.WidthTypes.Normal;
            this.ucLCResource.XAlign = 605;
            this.ucLCResource.SelectedIndexChanged += new System.EventHandler(this.ucLCResource_SelectedIndexChanged);
            // 
            // panelButton
            // 
            this.panelButton.Controls.Add(this.ucBtnDeleteINNO);
            this.panelButton.Controls.Add(this.ucBtnDelete);
            this.panelButton.Controls.Add(this.ucBtnExit);
            this.panelButton.Controls.Add(this.ucBtnSave);
            this.panelButton.Controls.Add(this.ucBtnUpdate);
            this.panelButton.Controls.Add(this.ucBtnAdd);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(0, 445);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(760, 56);
            this.panelButton.TabIndex = 3;
            // 
            // ucBtnDeleteINNO
            // 
            this.ucBtnDeleteINNO.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnDeleteINNO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnDeleteINNO.BackgroundImage")));
            this.ucBtnDeleteINNO.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnDeleteINNO.Caption = "删除集成上料号";
            this.ucBtnDeleteINNO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnDeleteINNO.Location = new System.Drawing.Point(440, 16);
            this.ucBtnDeleteINNO.Name = "ucBtnDeleteINNO";
            this.ucBtnDeleteINNO.Size = new System.Drawing.Size(88, 22);
            this.ucBtnDeleteINNO.TabIndex = 5;
            this.ucBtnDeleteINNO.Click += new System.EventHandler(this.ucBtnDeleteINNO_Click);
            // 
            // ucBtnDelete
            // 
            this.ucBtnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnDelete.BackgroundImage")));
            this.ucBtnDelete.ButtonType = UserControl.ButtonTypes.Delete;
            this.ucBtnDelete.Caption = "删除";
            this.ucBtnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnDelete.Location = new System.Drawing.Point(336, 16);
            this.ucBtnDelete.Name = "ucBtnDelete";
            this.ucBtnDelete.Size = new System.Drawing.Size(88, 22);
            this.ucBtnDelete.TabIndex = 2;
            this.ucBtnDelete.Click += new System.EventHandler(this.ucBtnDelete_Click);
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(640, 16);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 4;
            // 
            // ucBtnSave
            // 
            this.ucBtnSave.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnSave.BackgroundImage")));
            this.ucBtnSave.ButtonType = UserControl.ButtonTypes.Save;
            this.ucBtnSave.Caption = "保存";
            this.ucBtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnSave.Location = new System.Drawing.Point(536, 16);
            this.ucBtnSave.Name = "ucBtnSave";
            this.ucBtnSave.Size = new System.Drawing.Size(88, 22);
            this.ucBtnSave.TabIndex = 3;
            this.ucBtnSave.Click += new System.EventHandler(this.ucBtnSave_Click);
            // 
            // ucBtnUpdate
            // 
            this.ucBtnUpdate.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnUpdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnUpdate.BackgroundImage")));
            this.ucBtnUpdate.ButtonType = UserControl.ButtonTypes.Edit;
            this.ucBtnUpdate.Caption = "编辑";
            this.ucBtnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnUpdate.Location = new System.Drawing.Point(228, 16);
            this.ucBtnUpdate.Name = "ucBtnUpdate";
            this.ucBtnUpdate.Size = new System.Drawing.Size(88, 22);
            this.ucBtnUpdate.TabIndex = 1;
            this.ucBtnUpdate.Click += new System.EventHandler(this.ucBtnUpdate_Click);
            // 
            // ucBtnAdd
            // 
            this.ucBtnAdd.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnAdd.BackgroundImage")));
            this.ucBtnAdd.ButtonType = UserControl.ButtonTypes.Add;
            this.ucBtnAdd.Caption = "添加";
            this.ucBtnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnAdd.Location = new System.Drawing.Point(124, 16);
            this.ucBtnAdd.Name = "ucBtnAdd";
            this.ucBtnAdd.Size = new System.Drawing.Size(88, 22);
            this.ucBtnAdd.TabIndex = 0;
            this.ucBtnAdd.Click += new System.EventHandler(this.ucBtnAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBarcode);
            this.groupBox1.Controls.Add(this.chkCheckERPBOM);
            this.groupBox1.Controls.Add(this.lblMitemName);
            this.groupBox1.Controls.Add(this.ucLEItemCode);
            this.groupBox1.Controls.Add(this.ucLEBIOS);
            this.groupBox1.Controls.Add(this.ucLEPCBA);
            this.groupBox1.Controls.Add(this.ucLEVendorItem);
            this.groupBox1.Controls.Add(this.ucLEDateCode);
            this.groupBox1.Controls.Add(this.ucLETryItemCode);
            this.groupBox1.Controls.Add(this.ucLEVersion);
            this.groupBox1.Controls.Add(this.ucLEVendorCode);
            this.groupBox1.Controls.Add(this.ucLELotNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 277);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 168);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // txtBarcode
            // 
            this.txtBarcode.AllowEditOnlyChecked = true;
            this.txtBarcode.Caption = "物料条码";
            this.txtBarcode.Checked = false;
            this.txtBarcode.EditType = UserControl.EditTypes.String;
            this.txtBarcode.Location = new System.Drawing.Point(41, 24);
            this.txtBarcode.MaxLength = 40;
            this.txtBarcode.Multiline = false;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.PasswordChar = '\0';
            this.txtBarcode.ReadOnly = false;
            this.txtBarcode.ShowCheckBox = false;
            this.txtBarcode.Size = new System.Drawing.Size(461, 24);
            this.txtBarcode.TabIndex = 40;
            this.txtBarcode.TabNext = true;
            this.txtBarcode.Value = "";
            this.txtBarcode.WidthType = UserControl.WidthTypes.TooLong;
            this.txtBarcode.XAlign = 102;
            this.txtBarcode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_TxtboxKeyPress);
            // 
            // chkCheckERPBOM
            // 
            this.chkCheckERPBOM.Location = new System.Drawing.Point(536, 24);
            this.chkCheckERPBOM.Name = "chkCheckERPBOM";
            this.chkCheckERPBOM.Size = new System.Drawing.Size(136, 24);
            this.chkCheckERPBOM.TabIndex = 39;
            this.chkCheckERPBOM.Text = "检查工单发料资料";
            // 
            // lblMitemName
            // 
            this.lblMitemName.Location = new System.Drawing.Point(488, 64);
            this.lblMitemName.Name = "lblMitemName";
            this.lblMitemName.Size = new System.Drawing.Size(168, 23);
            this.lblMitemName.TabIndex = 38;
            // 
            // ucLEItemCode
            // 
            this.ucLEItemCode.AllowEditOnlyChecked = true;
            this.ucLEItemCode.Caption = "料号";
            this.ucLEItemCode.Checked = false;
            this.ucLEItemCode.EditType = UserControl.EditTypes.String;
            this.ucLEItemCode.Location = new System.Drawing.Point(288, 64);
            this.ucLEItemCode.MaxLength = 40;
            this.ucLEItemCode.Multiline = false;
            this.ucLEItemCode.Name = "ucLEItemCode";
            this.ucLEItemCode.PasswordChar = '\0';
            this.ucLEItemCode.ReadOnly = false;
            this.ucLEItemCode.ShowCheckBox = false;
            this.ucLEItemCode.Size = new System.Drawing.Size(170, 24);
            this.ucLEItemCode.TabIndex = 5;
            this.ucLEItemCode.TabNext = true;
            this.ucLEItemCode.Value = "";
            this.ucLEItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemCode.XAlign = 325;
            this.ucLEItemCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEItemCode_TxtboxKeyPress);
            // 
            // ucLEBIOS
            // 
            this.ucLEBIOS.AllowEditOnlyChecked = true;
            this.ucLEBIOS.Caption = "BIOS版本";
            this.ucLEBIOS.Checked = false;
            this.ucLEBIOS.EditType = UserControl.EditTypes.String;
            this.ucLEBIOS.Location = new System.Drawing.Point(481, 128);
            this.ucLEBIOS.MaxLength = 40;
            this.ucLEBIOS.Multiline = false;
            this.ucLEBIOS.Name = "ucLEBIOS";
            this.ucLEBIOS.PasswordChar = '\0';
            this.ucLEBIOS.ReadOnly = false;
            this.ucLEBIOS.ShowCheckBox = false;
            this.ucLEBIOS.Size = new System.Drawing.Size(194, 24);
            this.ucLEBIOS.TabIndex = 17;
            this.ucLEBIOS.TabNext = true;
            this.ucLEBIOS.Value = "";
            this.ucLEBIOS.Visible = false;
            this.ucLEBIOS.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEBIOS.XAlign = 542;
            // 
            // ucLEPCBA
            // 
            this.ucLEPCBA.AllowEditOnlyChecked = true;
            this.ucLEPCBA.Caption = "PCBA版本";
            this.ucLEPCBA.Checked = false;
            this.ucLEPCBA.EditType = UserControl.EditTypes.String;
            this.ucLEPCBA.Location = new System.Drawing.Point(265, 128);
            this.ucLEPCBA.MaxLength = 40;
            this.ucLEPCBA.Multiline = false;
            this.ucLEPCBA.Name = "ucLEPCBA";
            this.ucLEPCBA.PasswordChar = '\0';
            this.ucLEPCBA.ReadOnly = false;
            this.ucLEPCBA.ShowCheckBox = false;
            this.ucLEPCBA.Size = new System.Drawing.Size(194, 24);
            this.ucLEPCBA.TabIndex = 15;
            this.ucLEPCBA.TabNext = true;
            this.ucLEPCBA.Value = "";
            this.ucLEPCBA.Visible = false;
            this.ucLEPCBA.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEPCBA.XAlign = 326;
            // 
            // ucLEVendorItem
            // 
            this.ucLEVendorItem.AllowEditOnlyChecked = true;
            this.ucLEVendorItem.Caption = "厂商料号";
            this.ucLEVendorItem.Checked = false;
            this.ucLEVendorItem.EditType = UserControl.EditTypes.String;
            this.ucLEVendorItem.Location = new System.Drawing.Point(481, 96);
            this.ucLEVendorItem.MaxLength = 40;
            this.ucLEVendorItem.Multiline = false;
            this.ucLEVendorItem.Name = "ucLEVendorItem";
            this.ucLEVendorItem.PasswordChar = '\0';
            this.ucLEVendorItem.ReadOnly = false;
            this.ucLEVendorItem.ShowCheckBox = false;
            this.ucLEVendorItem.Size = new System.Drawing.Size(194, 24);
            this.ucLEVendorItem.TabIndex = 11;
            this.ucLEVendorItem.TabNext = true;
            this.ucLEVendorItem.Value = "";
            this.ucLEVendorItem.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEVendorItem.XAlign = 542;
            // 
            // ucLEDateCode
            // 
            this.ucLEDateCode.AllowEditOnlyChecked = true;
            this.ucLEDateCode.Caption = "生产日期";
            this.ucLEDateCode.Checked = false;
            this.ucLEDateCode.EditType = UserControl.EditTypes.String;
            this.ucLEDateCode.Location = new System.Drawing.Point(41, 96);
            this.ucLEDateCode.MaxLength = 40;
            this.ucLEDateCode.Multiline = false;
            this.ucLEDateCode.Name = "ucLEDateCode";
            this.ucLEDateCode.PasswordChar = '\0';
            this.ucLEDateCode.ReadOnly = false;
            this.ucLEDateCode.ShowCheckBox = false;
            this.ucLEDateCode.Size = new System.Drawing.Size(194, 24);
            this.ucLEDateCode.TabIndex = 7;
            this.ucLEDateCode.TabNext = true;
            this.ucLEDateCode.Value = "";
            this.ucLEDateCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEDateCode.XAlign = 102;
            // 
            // ucLETryItemCode
            // 
            this.ucLETryItemCode.AllowEditOnlyChecked = true;
            this.ucLETryItemCode.Caption = "试流料";
            this.ucLETryItemCode.Checked = false;
            this.ucLETryItemCode.EditType = UserControl.EditTypes.String;
            this.ucLETryItemCode.Location = new System.Drawing.Point(561, 152);
            this.ucLETryItemCode.MaxLength = 40;
            this.ucLETryItemCode.Multiline = false;
            this.ucLETryItemCode.Name = "ucLETryItemCode";
            this.ucLETryItemCode.PasswordChar = '\0';
            this.ucLETryItemCode.ReadOnly = false;
            this.ucLETryItemCode.ShowCheckBox = true;
            this.ucLETryItemCode.Size = new System.Drawing.Size(198, 24);
            this.ucLETryItemCode.TabIndex = 1;
            this.ucLETryItemCode.TabNext = true;
            this.ucLETryItemCode.Value = "";
            this.ucLETryItemCode.Visible = false;
            this.ucLETryItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLETryItemCode.XAlign = 626;
            // 
            // ucLEVersion
            // 
            this.ucLEVersion.AllowEditOnlyChecked = true;
            this.ucLEVersion.Caption = "物料版本";
            this.ucLEVersion.Checked = false;
            this.ucLEVersion.EditType = UserControl.EditTypes.String;
            this.ucLEVersion.Location = new System.Drawing.Point(41, 128);
            this.ucLEVersion.MaxLength = 40;
            this.ucLEVersion.Multiline = false;
            this.ucLEVersion.Name = "ucLEVersion";
            this.ucLEVersion.PasswordChar = '\0';
            this.ucLEVersion.ReadOnly = false;
            this.ucLEVersion.ShowCheckBox = false;
            this.ucLEVersion.Size = new System.Drawing.Size(194, 24);
            this.ucLEVersion.TabIndex = 13;
            this.ucLEVersion.TabNext = true;
            this.ucLEVersion.Value = "";
            this.ucLEVersion.Visible = false;
            this.ucLEVersion.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEVersion.XAlign = 102;
            // 
            // ucLEVendorCode
            // 
            this.ucLEVendorCode.AllowEditOnlyChecked = true;
            this.ucLEVendorCode.Caption = "厂商";
            this.ucLEVendorCode.Checked = false;
            this.ucLEVendorCode.EditType = UserControl.EditTypes.String;
            this.ucLEVendorCode.Location = new System.Drawing.Point(288, 96);
            this.ucLEVendorCode.MaxLength = 40;
            this.ucLEVendorCode.Multiline = false;
            this.ucLEVendorCode.Name = "ucLEVendorCode";
            this.ucLEVendorCode.PasswordChar = '\0';
            this.ucLEVendorCode.ReadOnly = false;
            this.ucLEVendorCode.ShowCheckBox = false;
            this.ucLEVendorCode.Size = new System.Drawing.Size(170, 24);
            this.ucLEVendorCode.TabIndex = 9;
            this.ucLEVendorCode.TabNext = true;
            this.ucLEVendorCode.Value = "";
            this.ucLEVendorCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEVendorCode.XAlign = 325;
            // 
            // ucLELotNo
            // 
            this.ucLELotNo.AllowEditOnlyChecked = true;
            this.ucLELotNo.Caption = "生产批号";
            this.ucLELotNo.Checked = false;
            this.ucLELotNo.EditType = UserControl.EditTypes.String;
            this.ucLELotNo.Location = new System.Drawing.Point(41, 64);
            this.ucLELotNo.MaxLength = 40;
            this.ucLELotNo.Multiline = false;
            this.ucLELotNo.Name = "ucLELotNo";
            this.ucLELotNo.PasswordChar = '\0';
            this.ucLELotNo.ReadOnly = false;
            this.ucLELotNo.ShowCheckBox = false;
            this.ucLELotNo.Size = new System.Drawing.Size(194, 24);
            this.ucLELotNo.TabIndex = 3;
            this.ucLELotNo.TabNext = true;
            this.ucLELotNo.Value = "";
            this.ucLELotNo.WidthType = UserControl.WidthTypes.Normal;
            this.ucLELotNo.XAlign = 102;
            this.ucLELotNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLELotNo_TxtboxKeyPress);
            // 
            // ultraGridINNO
            // 
            this.ultraGridINNO.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridINNO.DataSource = this.dtMINNO;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.Caption = "料号";
            ultraGridColumn2.Width = 128;
            ultraGridColumn3.Header.Caption = "生产批号";
            ultraGridColumn3.Header.VisiblePosition = 3;
            ultraGridColumn4.Header.Caption = "厂商";
            ultraGridColumn4.Header.VisiblePosition = 4;
            ultraGridColumn5.Header.Caption = "厂商料号";
            ultraGridColumn5.Header.VisiblePosition = 5;
            ultraGridColumn6.Header.Caption = "生产日期";
            ultraGridColumn6.Header.VisiblePosition = 6;
            ultraGridColumn7.Header.Caption = "物料版本";
            ultraGridColumn7.Header.VisiblePosition = 7;
            ultraGridColumn8.Header.Caption = "BIOS版本";
            ultraGridColumn8.Header.VisiblePosition = 8;
            ultraGridColumn9.Header.Caption = "PCBA版本";
            ultraGridColumn9.Header.VisiblePosition = 9;
            ultraGridColumn10.Header.Caption = "试流料";
            ultraGridColumn10.Header.VisiblePosition = 10;
            ultraGridColumn11.Header.Caption = "替代料";
            ultraGridColumn11.Header.VisiblePosition = 11;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn12.Header.VisiblePosition = 12;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn13.Header.Caption = "物料描述";
            ultraGridColumn13.Header.VisiblePosition = 2;
            ultraGridColumn13.Width = 133;
            ultraGridBand1.Columns.Add(ultraGridColumn1);
            ultraGridBand1.Columns.Add(ultraGridColumn2);
            ultraGridBand1.Columns.Add(ultraGridColumn3);
            ultraGridBand1.Columns.Add(ultraGridColumn4);
            ultraGridBand1.Columns.Add(ultraGridColumn5);
            ultraGridBand1.Columns.Add(ultraGridColumn6);
            ultraGridBand1.Columns.Add(ultraGridColumn7);
            ultraGridBand1.Columns.Add(ultraGridColumn8);
            ultraGridBand1.Columns.Add(ultraGridColumn9);
            ultraGridBand1.Columns.Add(ultraGridColumn10);
            ultraGridBand1.Columns.Add(ultraGridColumn11);
            ultraGridBand1.Columns.Add(ultraGridColumn12);
            ultraGridBand1.Columns.Add(ultraGridColumn13);
            this.ultraGridINNO.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance1.BackColor = System.Drawing.Color.WhiteSmoke;
            appearance1.FontData.BoldAsString = "True";
            appearance1.TextHAlign = Infragistics.Win.HAlign.Left;
            this.ultraGridINNO.DisplayLayout.CaptionAppearance = appearance1;
            this.ultraGridINNO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridINNO.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridINNO.Location = new System.Drawing.Point(0, 80);
            this.ultraGridINNO.Name = "ultraGridINNO";
            this.ultraGridINNO.Size = new System.Drawing.Size(760, 197);
            this.ultraGridINNO.TabIndex = 1;
            this.ultraGridINNO.Text = "当前集成上料号:";
            this.ultraGridINNO.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.ultraGridINNO_AfterSelectChange);
            // 
            // FINNO
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(760, 501);
            this.Controls.Add(this.ultraGridINNO);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelButton);
            this.Controls.Add(this.grpQuery);
            this.Name = "FINNO";
            this.Text = "集成备料";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FINNO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtMINNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.grpQuery.ResumeLayout(false);
            this.panelButton.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridINNO)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

//		private Hashtable _srcItemHt = null;
		private bool _isDirty = false;
		private string _moCode			= string.Empty;
		private string _itemCode		= string.Empty;
		private string _routeCode		= string.Empty;
		private string _operationCode	= string.Empty;
		private string _resourceCode	= string.Empty;
		
		protected void ShowMessage(string message)
		{
			///lablastMsg.Text =message;
			ApplicationRun.GetInfoForm().Add( message );
		}

		protected void ShowMessage(Messages messages)
		{			
			ApplicationRun.GetInfoForm().Add(messages);
		}

		protected void ShowMessage(UserControl.Message message)
		{			
			ApplicationRun.GetInfoForm().Add(message);
		}
		
		private void FINNO_Load(object sender, System.EventArgs e)
		{
			this.ucLCToRes.Enabled = false;
			this.ucBtnCopy.Enabled = false;		
	
			this.status = FormStatus.Noready;
		}

		#region Form Status
		private string _status = FormStatus.Ready;
		private string status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;

				if ( this._status == FormStatus.Noready )
				{
					this.ucBtnAdd.Enabled	 = false;
					this.ucBtnDelete.Enabled = false;
					this.ucBtnUpdate.Enabled = false;
				
					this.setEditObject(null);
					this.enableEditPanel(false);
					
					this.ucLCToRes.Enabled = false;
					this.ucBtnCopy.Enabled = false;

					this.ucBtnSave.Enabled = false;
				}
				else
				{
					this.ucLCToRes.Enabled = true;
					this.ucBtnCopy.Enabled = true;

					this.ucBtnSave.Enabled = true;
				}

				if ( this._status == FormStatus.Ready )
				{
					this.ucBtnAdd.Enabled = true;
					this.ucBtnDelete.Enabled = false;
					this.ucBtnUpdate.Enabled = false;

					this.setEditObject(null);
					this.enableEditPanel(false);

//					this.buildItemList();
				}
				if ( this._status == FormStatus.Add )
				{
					this.ucBtnAdd.Enabled = true;
					this.ucBtnDelete.Enabled = false;
					this.ucBtnUpdate.Enabled = false;

					this.ucLEItemCode.TextFocus(false, true);
					this.setEditObject(null);

					this.enableEditPanel(true);
					this._isDirty = true;
				}
				if ( this._status == FormStatus.Update )
				{
					this.ucBtnAdd.Enabled = false;
					this.ucBtnDelete.Enabled = true;
					this.ucBtnUpdate.Enabled = true;

					this.ucLETryItemCode.TextFocus(false, true);

					this.enableEditPanel(true);
					this.ucLEItemCode.Enabled = false;
					this._isDirty = true;
				}
			}
		}
		#endregion

		#region DropdownList Data
		private void ucLEMO_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar == '\r')
			{
				if ( !this.confirmChangeQuery() )
				{
					this.ucLEMO.Value = this._moCode;
					return;
				}

				this.ucLCRoute.Clear();
				this.ucLCRoute.AddItem("", "");	
			
				if ( this.ucLEMO.Value.Trim() != string.Empty )
				{
					MOFacade moFacade = new MOFacade( this.DataProvider );
					object obj = moFacade.GetMO( this.ucLEMO.Value.Trim().ToUpper() );

					if ( obj == null )
					{
						this.ShowMessage( new UserControl.Message(MessageType.Error,"$Error_CS_MO_Not_Exist"));
					}
					else if ( ((MO)obj).MOStatus != MOManufactureStatus.MOSTATUS_RELEASE && ((MO)obj).MOStatus != MOManufactureStatus.MOSTATUS_OPEN )
					{
						this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_MO_Should_be_Release_or_Open"));
					}
					else
					{
						this._moCode = ((MO)obj).MOCode;
						this._itemCode = ((MO)obj).ItemCode;

						object[] objs = moFacade.GetNormalAndReworkRouteByMOCode( this._moCode );

						if ( objs != null )
						{
							foreach ( Route route in objs )
							{
								this.ucLCRoute.AddItem( route.RouteCode, route.RouteCode );
							}
						}
					}
				}

				this.ucLCRoute_SelectedIndexChanged( sender, e );
			}
		}

		private void ucLCRoute_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( this.ucLCRoute.SelectedIndex > 0 &&  this._routeCode == this.ucLCRoute.SelectedItemValue.ToString() )
			{
				return;
			}

			if ( !this.confirmChangeQuery() )
			{
				this.ucLCRoute.SetSelectItem( this._routeCode );
				return;
			}

			this.ucLCOp.Clear();
			this.ucLCOp.AddItem("", "");	
			this._routeCode	= string.Empty;
		
			if ( this.ucLCRoute.SelectedIndex > 0 )
			{
				this._routeCode = this.ucLCRoute.SelectedItemValue.ToString();

				object[] objs = new ItemFacade( this.DataProvider ).GetComponenetLoadingOperations( 
															this._itemCode,
															this._routeCode );

				if ( objs != null )
				{
					foreach ( ItemRoute2OP op in objs )
					{
						this.ucLCOp.AddItem( op.OPCode, op.OPCode );
					}
				}
			}

			this.ucLCOp_SelectedIndexChanged( sender, e );
		}

		private void ucLCOp_SelectedIndexChanged(object sender, System.EventArgs e)
		{	
			if ( this.ucLCOp.SelectedIndex > 0 && this._operationCode == this.ucLCOp.SelectedItemValue.ToString() )
			{
				return;
			}
			
			if ( !this.confirmChangeQuery() )
			{
				this.ucLCOp.SetSelectItem( this._operationCode );
				return;
			}

			this.ucLCResource.Clear();
			this.ucLCResource.AddItem("", "");	
			this._operationCode = string.Empty;

			if ( this.ucLCOp.SelectedIndex > 0 )
			{				
				this._operationCode = this.ucLCOp.SelectedItemValue.ToString();

				object[] objs = new BaseModelFacade( this.DataProvider ).GetResourceByOperationCode( this.ucLCOp.SelectedItemValue.ToString() );

				if ( objs != null )
				{
					foreach ( Resource resource in objs )
					{
						this.ucLCResource.AddItem( resource.ResourceCode, resource.ResourceCode );
					}
				}

//				objs = new OPBOMFacade( this.DataProvider ).GetLotControlOPBOMDetails( 
//					this._moCode, 
//					this.ucLCRoute.SelectedItemValue.ToString(), 
//					this.ucLCOp.SelectedItemValue.ToString() );
//
//				this._srcItemHt = new Hashtable();
//
//				if ( objs != null )
//				{
//					foreach ( OPBOMDetail opbom in objs )
//					{
//						if ( !this._srcItemHt.Contains(opbom.OPBOMItemCode) )
//						{
//							this._srcItemHt.Add(opbom.OPBOMItemCode, opbom.OPBOMSourceItemCode);
//						}
//					}
//				}
			}

			this.ucLCResource_SelectedIndexChanged( sender, e );
		}

		private void ucLCResource_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( this.ucLCResource.SelectedIndex > 0 && this._resourceCode == this.ucLCResource.SelectedItemValue.ToString() )
			{
				return;
			}
			
			if ( !this.confirmChangeQuery() )
			{
				this.ucLCResource.SetSelectItem( this._resourceCode );
				return;
			}

			if ( this.ucLCResource.SelectedIndex < 1 )
			{
				this.ucLCToRes.SelectedIndex = -1;
				this.status = FormStatus.Noready;
				this._resourceCode = string.Empty;
			}
			else
			{
				this._resourceCode = this.ucLCResource.SelectedItemValue.ToString();

				this.ucLCToRes.Clear();
				this.ucLCToRes.AddItem("","");

				object[] objs = new BaseModelFacade( this.DataProvider ).GetResourceByOperationCode( this.ucLCOp.SelectedItemValue.ToString() );

				if ( objs != null )
				{
					foreach ( Resource resource in objs )
					{
						if ( resource.ResourceCode != this.ucLCResource.SelectedItemValue.ToString() )
						{
							this.ucLCToRes.AddItem( resource.ResourceCode, resource.ResourceCode );
						}
					}
				}	

				this.status = FormStatus.Ready;
			}
            
			this.requestData();
		}

//		private void buildItemList()
//		{		
//			this.ucLCItemCode.Clear();
//			this.ucLCItemCode.AddItem("", "");	
//		
//			if ( this._moCode == string.Empty || this.ucLCRoute.SelectedIndex < 1 || this.ucLCOp.SelectedIndex < 1 )
//			{
//				return;
//			}		
//
//			ArrayList arrayItem = new ArrayList();		
//			ArrayList arraySrcItem = new ArrayList();
//			
//			foreach ( DataRow row in this.dtMINNO.Rows )
//			{
//				arrayItem.Add( row.ItemArray[1].ToString() );
//
//				if ( row.ItemArray[10].ToString() != string.Empty )
//				{
//					arraySrcItem.Add( row.ItemArray[10].ToString() );
//				}
//			}
//
//			// 排除Grid中已出现及替代料相同的的OPBOMItemCode
//			foreach (string key in this._srcItemHt.Keys)
//			{
//				if ( arrayItem.Contains(key) || arraySrcItem.Contains(this._srcItemHt[key].ToString()) )
//				{
//					continue;
//				}
//					
//				this.ucLCItemCode.AddItem( key, key );
//			}
//
//		}
		#endregion

		#region DataSource
		private object[] loadDataSource()
		{
			return this._facade.QueryLastMINNO( this._moCode, 
				this.ucLCRoute.SelectedItemValue.ToString(), 
				this.ucLCOp.SelectedItemValue.ToString(),
				this.ucLCResource.SelectedItemValue.ToString() );
		}

		private object[] getRow( object obj )
		{
			if ( obj == null )
			{
				return new object[]{"","","","","","","","","","","",""};
			}

			return new object[] {
									((MINNO)obj).Sequence,
									((MINNO)obj).MItemCode,
									((MINNO)obj).LotNO,
									((MINNO)obj).VendorCode,
									((MINNO)obj).VendorItemCode,
									((MINNO)obj).DateCode,
									((MINNO)obj).Version,
									((MINNO)obj).BIOS,
									((MINNO)obj).PCBA,
									((MINNO)obj).TryItemCode,
									((MINNO)obj).MSourceItemCode,
									((MINNO)obj).MItemName
								};
		}

		private void gridBind()
		{
			this.dtMINNO.Rows.Clear();

			object[] objs = this.loadDataSource();
			
			if ( objs == null )
			{
				this.ShowMessage("$CS_Please_Add_INNO_Info");	//当前资源下没有任何集成上料资料，请新增

				this.ultraGridINNO.Text = "当前集成上料号:";	
			}
			else
			{
				foreach ( object obj in objs )
				{
					this.dtMINNO.Rows.Add( this.getRow(obj) );
				}
				
				this.ultraGridINNO.Text = string.Format("当前集成上料号:{0}", ((MINNO)objs[0]).INNO );	
			}		
		}
		#endregion

		#region Function
		private void requestData()
		{
			if ( this._moCode == string.Empty || this.ucLCRoute.SelectedIndex < 1 || 
				this.ucLCOp.SelectedIndex < 1 || this.ucLCResource.SelectedIndex < 1 )
			{
				this.dtMINNO.Rows.Clear();
				this.ultraGridINNO.Text = string.Format("当前集成上料号:");	
				this.status = FormStatus.Noready;
			}
			else
			{
				this.gridBind();
				this.status = FormStatus.Ready;

//				this.buildItemList();
			}

			this._isDirty = false;
		}

		private void setGridRow( object obj )
		{
			this.ultraGridINNO.ActiveRow.Cells[0].Value = ((MINNO)obj).Sequence;
			this.ultraGridINNO.ActiveRow.Cells[1].Value = ((MINNO)obj).MItemCode;
			this.ultraGridINNO.ActiveRow.Cells[12].Value = ((MINNO)obj).MItemName;
			this.ultraGridINNO.ActiveRow.Cells[2].Value = ((MINNO)obj).LotNO;
			this.ultraGridINNO.ActiveRow.Cells[3].Value = ((MINNO)obj).VendorCode;
			this.ultraGridINNO.ActiveRow.Cells[4].Value = ((MINNO)obj).VendorItemCode;
			this.ultraGridINNO.ActiveRow.Cells[5].Value = ((MINNO)obj).DateCode;
			this.ultraGridINNO.ActiveRow.Cells[6].Value = ((MINNO)obj).Version;
			this.ultraGridINNO.ActiveRow.Cells[7].Value = ((MINNO)obj).BIOS;
			this.ultraGridINNO.ActiveRow.Cells[8].Value = ((MINNO)obj).PCBA;
			this.ultraGridINNO.ActiveRow.Cells[9].Value = ((MINNO)obj).TryItemCode;
			this.ultraGridINNO.ActiveRow.Cells[10].Value = ((MINNO)obj).MSourceItemCode;
		}

		private void enableEditPanel( bool enable )
		{
			this.ucLEItemCode.Enabled	 = enable;
			this.ucLETryItemCode.Enabled = enable;
			this.ucLETryItemCode.Enabled = enable;
			this.ucLELotNo.Enabled		 = enable;
			this.ucLEDateCode.Enabled	 = enable;
			this.ucLEVendorCode.Enabled  = enable;
			this.ucLEVendorItem.Enabled  = enable;
			this.ucLEVersion.Enabled	 = enable;
			this.ucLEBIOS.Enabled		 = enable;
			this.ucLEPCBA.Enabled		 = enable;
			this.txtBarcode.Enabled		 = enable;
			this.chkCheckERPBOM.Enabled	 = enable;
		}

//		private void enableQueryPanel( bool enable )
//		{
////			this.ucLEMO.Enabled		  = enable;
////			this.ucLCRoute.Enabled	  = enable;
////			this.ucLCOp.Enabled		  = enable;
////			this.ucLCResource.Enabled = enable;
////
////			this.ucBtnClear.Enabled	  = !enable;
//
//			this._isDirty = !enable;
//		}

		private bool confirmChangeQuery()
		{
			if ( !this._isDirty )
			{
				return true;

			}

			if ( MessageBox.Show(this, "改变查询条件将丢失所有未保存的操作,是否确定?", this.Text, MessageBoxButtons.OKCancel) == DialogResult.OK )
			{
				this._isDirty = false;
				return true;
			}
            
			return false;
		}
		#endregion

		#region Object <--> Form
		protected object getEditObject()
		{			
			if ( this.validateInput() == false )
			{
				return null;
			}
			
			
			MINNO mINNO = this._facade.CreateNewMINNO();

			OPBOMDetail opbom = this.getOPBOM( this.ucLEItemCode.Value.Trim().ToUpper() );

			if ( opbom == null )
			{
				return null;
			}

			//AMOI  MARK  START  20050803 如果是非管控物料,则必须输入试流料
			if (opbom.OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_NOCONTROL )
			{
				if ((!this.ucLETryItemCode.Checked)||
					(this.ucLETryItemCode.Value.Trim()==string.Empty))
				{
					this.ShowMessage("$CS_NOCONTROLOPBOMItem_Must_Input_TryItem");
					return null;
				}
			}
			//AMOI  MARK  END

			if ( opbom.OPBOMSourceItemCode == string.Empty )
			{
				mINNO.MSourceItemCode = opbom.OPBOMItemCode;
			}
			else
			{
				mINNO.MSourceItemCode = opbom.OPBOMSourceItemCode;			
			}

			lblMitemName.Text = opbom.OPBOMItemName;
			mINNO.MItemCode		  = this.ucLEItemCode.Value.Trim().ToUpper();
			mINNO.IsTry			  = this.ucLETryItemCode.Checked ? "Y" : "N";
			mINNO.LotNO			  = this.ucLELotNo.Value.Trim(); 
			mINNO.DateCode		  = this.ucLEDateCode.Value.Trim();
			mINNO.VendorCode	  = this.ucLEVendorCode.Value.Trim(); 
			mINNO.VendorItemCode  = this.ucLEVendorItem.Value.Trim(); 
			mINNO.Version		  = this.ucLEVersion.Value.Trim(); 
			mINNO.BIOS			  = this.ucLEBIOS.Value.Trim(); 
			mINNO.PCBA			  = this.ucLEPCBA.Value.Trim();
			mINNO.MItemName		  = lblMitemName.Text.Trim().ToUpper();

			if ( this.ucLETryItemCode.Checked )
			{
				mINNO.TryItemCode = this.ucLETryItemCode.Value.Trim();
			}
			else
			{
				mINNO.TryItemCode = string.Empty;
			}

//			if ( this._srcItemHt.Contains(mINNO.MItemCode) )
//			{
//				mINNO.MSourceItemCode = this._srcItemHt[mINNO.MItemCode].ToString();
//			}
//			else
//			{
//				mINNO.MSourceItemCode = string.Empty;
//			}

			return mINNO;
		}

		protected object getEditObject( Infragistics.Win.UltraWinGrid.UltraGridRow row )
		{
			MINNO mINNO = this._facade.CreateNewMINNO();

			mINNO.MItemCode		= row.Cells[1].Text.Trim();
			mINNO.MItemName		= row.Cells[12].Text.Trim();
			mINNO.LotNO			= row.Cells[2].Text.Trim();
			mINNO.VendorCode	= row.Cells[3].Text.Trim();
			mINNO.VendorItemCode = row.Cells[4].Text.Trim();
			mINNO.DateCode		= row.Cells[5].Text.Trim();
			mINNO.Version		= row.Cells[6].Text.Trim();
			mINNO.BIOS			= row.Cells[7].Text.Trim();
			mINNO.PCBA			= row.Cells[8].Text.Trim();
			mINNO.TryItemCode	= row.Cells[9].Text.Trim();
			mINNO.IsTry			= row.Cells[9].Text.Trim() == string.Empty ? "N" : "Y";
			mINNO.MaintainUser	  = ApplicationService.Current().UserCode;

			return mINNO;
		}

		protected void setEditObject(object obj)
		{
			if (obj == null)
			{
				this.ucLEItemCode.Value = "";
				this.ucLETryItemCode.Checked = false;
				this.ucLETryItemCode.Value = "";
				this.ucLELotNo.Value = ""; 
				this.ucLEDateCode.Value = "";
				this.ucLEVendorCode.Value = ""; 
				this.ucLEVendorItem.Value = ""; 
				this.ucLEVersion.Value = ""; 
				this.ucLEBIOS.Value = ""; 
				this.ucLEPCBA.Value = ""; 

				return;
			}

			this.ucLEItemCode.Value		= ((MINNO)obj).MItemCode;
			this.ucLETryItemCode.Checked = ((MINNO)obj).IsTry == "Y" ? true : false;
			this.ucLETryItemCode.Value  = ((MINNO)obj).TryItemCode;		
			this.ucLELotNo.Value		= ((MINNO)obj).LotNO;	
			this.ucLEDateCode.Value		= ((MINNO)obj).DateCode;	
			this.ucLEVendorCode.Value	= ((MINNO)obj).VendorCode; 
			this.ucLEVendorItem.Value	= ((MINNO)obj).VendorItemCode;
			this.ucLEVersion.Value		= ((MINNO)obj).Version;
			this.ucLEBIOS.Value			= ((MINNO)obj).BIOS;	
			this.ucLEPCBA.Value			= ((MINNO)obj).PCBA;
			lblMitemName.Text			= ((MINNO)obj).MItemName;
		}
		
		private bool validateInput()
		{
			bool validate = true;

			if ( this.ucLELotNo.Value.Trim() == string.Empty )
			{
				this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_LotNo_Empty"));
				//Application.DoEvents();
				ucLELotNo.TextFocus(false, true);

				validate = false;
			}

			if ( this.ucLEItemCode.Value.Trim() == string.Empty )
			{
				this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_ItemCode_Empty"));
				//Application.DoEvents();
				ucLELotNo.TextFocus(false, true);

				validate = false;
			}
			if ( this.ucLETryItemCode.Checked && this.ucLETryItemCode.Value.Trim() == string.Empty )
			{
				this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_Try_ItemCode_Empty"));
				//Application.DoEvents();
				ucLELotNo.TextFocus(false, true);

				validate = false;
			}
			//Laws Lu,2005/08/12,注释
//			if (this.ucLELotNo.Value.Trim() == string.Empty )
//			{
//				this.ShowMessage("$Error_CS_LotNo_Empty");
//				validate = false;
//			}
			
			return validate;
		}

		private bool checkAddMINNO( MINNO mINNO )
		{
			foreach ( DataRow row in this.dtMINNO.Rows )
			{
				if ( row.ItemArray[1].ToString() == string.Empty )
				{
					continue;
				}

				if ( mINNO.MItemCode.ToUpper() == row.ItemArray[1].ToString().ToUpper() )
				{
					this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_Item_Is_Already_Exist"));
					return false;
				}

				if ( mINNO.MItemCode.ToUpper() == row.ItemArray[10].ToString().ToUpper() )
				{
					this.ShowMessage(new UserControl.Message(MessageType.Error,string.Format("$Error_CS_Substitute_Item_Is_Already_Exist[$OPBOMItemcode={0}]", row.ItemArray[1].ToString() )));
					return false;
				}

				if ( mINNO.MSourceItemCode != string.Empty )
				{
					if ( mINNO.MSourceItemCode.ToUpper() == row.ItemArray[10].ToString().ToUpper() || mINNO.MSourceItemCode.ToUpper() == row.ItemArray[1].ToString().ToUpper() )
					{
						this.ShowMessage(new UserControl.Message(MessageType.Error,string.Format("$Error_CS_SourceItem_Is_Already_Exist[$OPBOMItemcode={0}, $OPBOMSourceItemCode={1}]", row.ItemArray[1].ToString(), mINNO.MSourceItemCode)));
						return false;
					}
				}
			}

			return true;
		}

		private OPBOMDetail getOPBOM( string mItemCode )
		{	
			Messages messages = new Messages();
			
			OPBOMDetail opbom = this._facade.GetOPBOM( this._moCode, 
											this.ucLCRoute.SelectedItemValue.ToString(), 
											this.ucLCOp.SelectedItemValue.ToString(), 
											this.ucLCResource.SelectedItemValue.ToString(),
											mItemCode.ToUpper(),
											messages);

			if ( !messages.IsSuccess() )
			{
				this.ShowMessage( messages );
			}

			return opbom;
		}
		#endregion

		#region Button Event
		private void ucBtnAdd_Click(object sender, System.EventArgs e)
		{
			if ( this.status == FormStatus.Add )
			{	
				if(!validateInput())
				{
					return;
				}
				
				// Added by Icyer 2006/11/02
				if (this.chkCheckERPBOM.Checked == true)
				{
					MOFacade moFac = new MOFacade(DataProvider);
					object[] objERPBOMs =  moFac.QueryERPBOM(ucLEMO.Value.Trim(),ucLELotNo.Value.Trim());
					if(objERPBOMs == null || (objERPBOMs != null && objERPBOMs.Length < 1))
					{
						this.ShowMessage(new UserControl.Message(MessageType.Error,"$CS_ITEM_NOT_EXIST_ERPBOM"));

						//Application.DoEvents();
						ucLELotNo.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;

						return;
					}
				
					CheckERPBOM(objERPBOMs);
				}

				object obj = this.getEditObject();

				if ( obj == null )
				{
					return;
				}

				if ( !this.checkAddMINNO((MINNO)obj) )
				{
					return;
				}

				
				this.setGridRow( obj );
				this.status = FormStatus.Ready;
			}

			if ( this.status == FormStatus.Ready )
			{
				this.dtMINNO.Rows.Add( this.getRow(null) );

				this.ultraGridINNO.ActiveRow = this.ultraGridINNO.Rows[this.ultraGridINNO.Rows.Count - 1];
				this._emptyRow = this.ultraGridINNO.ActiveRow;

				this.status = FormStatus.Add;
			}

			lblMitemName.Text = String.Empty;

			if ( this.status == FormStatus.Add )
			{	
				//Laws Lu,2006/12/20 将焦点设置到条码输入框
				//Application.DoEvents();
				txtBarcode.TextFocus(true, true);
			}
		}

		private void ucBtnUpdate_Click(object sender, System.EventArgs e)
		{
			OPBOMDetail opbom = this.getOPBOM( this.ucLEItemCode.Value.Trim().ToUpper() );

			if ( opbom == null )
			{
				return;
			}

			object obj = this.getEditObject();

			if ( obj == null )
			{
				return;
			}

			((MINNO)obj).MSourceItemCode = opbom.OPBOMSourceItemCode;

			this.setGridRow( obj );            
			this.status = FormStatus.Ready;
		}

		private void ucBtnDelete_Click(object sender, System.EventArgs e)
		{
            if (MessageBox.Show(MutiLanguages.ParserMessage("$ConformDelete"), this.Text, MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			
			this.ultraGridINNO.ActiveRow.Delete(false);

			this.status = FormStatus.Ready;	
		}

		private string GetINNO()
		{
			return this.ultraGridINNO.Text.Substring(this.ultraGridINNO.Text.IndexOf(":") + 1).TrimEnd();
		}

		private void ucBtnSave_Click(object sender, System.EventArgs e)
		{
			if ( this._moCode == string.Empty || this.ucLCRoute.SelectedIndex < 1 || 
				this.ucLCOp.SelectedIndex < 1 || this.ucLCResource.SelectedIndex < 1 )
			{
				return;
			}
		
			//Added by Karron Qiu,2005-9-20
			//改变集成上料备料维护逻辑：修改集成上料资料后执行保持时检查集成上料号是否被使用过，
			//如果没被使用过则直接保持更改后的资料，不需要输入新的集成上料号
			string INNO = string.Empty;
			if(!this._facade.IsINNOHasBeenUsed(GetINNO(),this._moCode))
			{
				INNO = GetINNO();
			}

			if(INNO == string.Empty)
			{
				FMetrialINNOInput form = new FMetrialINNOInput();

				if ( form.ShowDialog(this) == DialogResult.OK )
				{
					INNO =form.INNO;
				}
			}

			//if ( form.ShowDialog(this) == DialogResult.OK )
			if(INNO != string.Empty)
			{
				ArrayList array = new ArrayList( this.ultraGridINNO.Rows.Count );

//				foreach ( Infragistics.Win.UltraWinGrid.UltraGridRow row in this.ultraGridINNO.Rows )
				for ( int iGridRowLoopIndex = 0; iGridRowLoopIndex < this.ultraGridINNO.Rows.Count; iGridRowLoopIndex++)
				{
					Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridINNO.Rows[iGridRowLoopIndex];
					

					if ( row.Cells[0].Text.Trim() != string.Empty )
					{
						array.Add( this.getEditObject(row) );
					}
				}

				Messages messages = this._facade.AddMINNOs( 
															//form.INNO, 
															INNO,
															this._moCode, 
															this.ucLCRoute.SelectedItemValue.ToString(), 
															this.ucLCOp.SelectedItemValue.ToString(), 
															this.ucLCResource.SelectedItemValue.ToString(), 
															array.ToArray() );

				if ( !messages.IsSuccess() )
				{
					this.ShowMessage( messages );
					return;
				}

				this.requestData();

				lblMitemName.Text = String.Empty;
				this.ShowMessage(new UserControl.Message(MessageType.Success,"$CS_Save_Success"));			
			}
		}

		private void ultraGridINNO_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
		{
			if ( e.Type != typeof(Infragistics.Win.UltraWinGrid.UltraGridRow) )
			{
				return;
			}

			// 添加状态下点击行，删除新增的空行
			if ( this.status == FormStatus.Add && this._emptyRow != null )
			{
				this._emptyRow.Delete(false);
			}

			if ( this.ultraGridINNO.ActiveRow == this._emptyRow || this.ultraGridINNO.ActiveRow == null )
			{
				this.status = FormStatus.Ready;
				return;
			}
				
			object obj = this.getEditObject(this.ultraGridINNO.ActiveRow);

			if ( obj == null )
			{
				return;
			}

			this.setEditObject( obj );
			this.status = FormStatus.Update;
			this.ultraGridINNO.ActiveRow.Selected = false;

//			if ( this._srcItemHt.Contains(((MINNO)obj).MItemCode) )
//			{
//				this.buildItemList();
//				this.ucLCItemCode.AddItem( ((MINNO)obj).MItemCode, ((MINNO)obj).MItemCode );
//
//				this.setEditObject( obj );                
//				this.status = FormStatus.Update;
//			}
//			else
//			{
//				this.status = FormStatus.Ready;
//				this.ShowMessage("料号不存在，请删除");
//				return;
//			}
		}

		private void ucBtnCopy_Click(object sender, System.EventArgs e)
		{			
			if ( this._moCode == string.Empty || this.ucLCRoute.SelectedIndex < 1 || 
				this.ucLCOp.SelectedIndex < 1 || this.ucLCResource.SelectedIndex < 1 )
			{
				return;
			}

			if ( this.ucLCToRes.SelectedIndex < 1 )
			{
				this.ShowMessage("$CS_Please_Input_Copy_To_Resource");//请输入复制资源代码
				return;
			}

			FMetrialINNOInput form = new FMetrialINNOInput();

			if ( form.ShowDialog(this) == DialogResult.OK )
			{
				this._facade.CopyMINNOToResource(form.INNO, 
												this._moCode, 
												this.ucLCRoute.SelectedItemValue.ToString(), 
												this.ucLCOp.SelectedItemValue.ToString(), 
												this.ucLCResource.SelectedItemValue.ToString(),
												this.ucLCToRes.SelectedItemValue.ToString() );
				
				this.ShowMessage(new UserControl.Message(MessageType.Success,"$CS_Copy_Success"));//复制成功
			}
		}

		private void ucLEItemCode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar == '\r' )
			{
				if ( this.ucLEItemCode.Value.Trim() == string.Empty )
				{
					return;
				}				

				OPBOMDetail opbom = this.getOPBOM( this.ucLEItemCode.Value.Trim().ToUpper() );

				if ( opbom == null )
				{
					return;
				}

				MINNO mINNO = this._facade.CreateNewMINNO();
				mINNO.MItemCode = opbom.OPBOMItemCode;
				mINNO.MSourceItemCode = opbom.OPBOMSourceItemCode;
				mINNO.MItemName = opbom.OPBOMItemName;

				lblMitemName.Text = opbom.OPBOMItemName;

				if ( !this.checkAddMINNO(mINNO) )
				{
					return;
				}				
			}
		}
		#endregion

		private void FINNO_Closed(object sender, System.EventArgs e)
		{
			if (this.DataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();  
		}

		private void ucBtnDeleteINNO_Click(object sender, System.EventArgs e)
		{

			string INNO = this.GetINNO();
			if(INNO == string.Empty)
			{
				this.ShowMessage("$Error_CS_INNO_IS_EMPTY");//INNO为空
				return;
			}

			if(MessageBox.Show(this,"将删除当前的集成上料号,确定吗?",this.Text,MessageBoxButtons.OKCancel) == DialogResult.Cancel)
				return;

			if(this._facade.IsINNOHasBeenUsed(INNO,this._moCode))
			{
				this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_INNO_HAS_BEEN_USED"));//INNO已经被使用
				return;
			}


			Messages messages = this._facade.DeleteINNO(INNO);

			if ( !messages.IsSuccess() )
			{
				this.ShowMessage( messages );
				return;
			}

			this.ShowMessage(new UserControl.Message(MessageType.Success,"$CS_Delete_Success"));

			this.dtMINNO.Rows.Clear();
//			this.ultraGridINNO.Text = string.Format("当前集成上料号:{0}",this._facade.GetLastINNO(
//				this._moCode, 
//				this.ucLCRoute.SelectedItemValue.ToString(), 
//				this.ucLCOp.SelectedItemValue.ToString(),
//				this.ucLCResource.SelectedItemValue.ToString()));	
			string lastINNO = this._facade.GetLastINNO(
				this._moCode, 
				this.ucLCRoute.SelectedItemValue.ToString(), 
				this.ucLCOp.SelectedItemValue.ToString(),
				this.ucLCResource.SelectedItemValue.ToString());	

			this._facade.ChangeLastMINNOStatus
				(
				lastINNO,
				this._moCode, 
				this.ucLCRoute.SelectedItemValue.ToString(), 
				this.ucLCOp.SelectedItemValue.ToString(),
				this.ucLCResource.SelectedItemValue.ToString()
				);

			this.status = FormStatus.Ready;
			this._isDirty = false;
			this.requestData();
		}

		private void CheckERPBOM(object[] objERPBOMs)
		{
			ERPBOM erpBOM = objERPBOMs[0] as ERPBOM;

			ucLEItemCode.Value = erpBOM.BITEMCODE;

			int iStartPositon = 2;
			int iDateCodeLength = 6;
			if(System.Configuration.ConfigurationSettings.AppSettings["DateCodeStartPiont"] != null
				&& System.Configuration.ConfigurationSettings.AppSettings["DateCodeStartPiont"].Trim() != String.Empty)
			{
				iStartPositon = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["DateCodeStartPiont"].Trim());
			}
			if(System.Configuration.ConfigurationSettings.AppSettings["DateCodeLength"] != null
				&& System.Configuration.ConfigurationSettings.AppSettings["DateCodeLength"].Trim() != String.Empty)
			{
				iDateCodeLength = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["DateCodeLength"].Trim());
			}

			if(erpBOM.LOTNO.Length >= iDateCodeLength)
			{
				ucLEDateCode.Value = erpBOM.LOTNO.Substring(iStartPositon,iDateCodeLength);
			}				
		}

		private void ucLELotNo_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				// Added by Icyer 2006/11/02
				if (this.chkCheckERPBOM.Checked == true)
				{
					MOFacade moFac = new MOFacade(DataProvider);
					object[] objERPBOMs =  moFac.QueryERPBOM(ucLEMO.Value.Trim(),ucLELotNo.Value.Trim());
					if(objERPBOMs == null || (objERPBOMs != null && objERPBOMs.Length < 1))
					{
						this.ShowMessage(new UserControl.Message(MessageType.Error,"$CS_ITEM_NOT_EXIST_ERPBOM"));

						//Application.DoEvents();
						ucLELotNo.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;

						return;
					}
					else
					{
						Hashtable htQty = new Hashtable();
						foreach(ERPBOM erpBOM in objERPBOMs)
						{
							if(htQty.ContainsKey(erpBOM.BITEMCODE))
							{
								htQty[erpBOM.BITEMCODE] = Decimal.Parse(htQty[erpBOM.BITEMCODE].ToString()) + erpBOM.BQTY;
							}
							else
							{
								htQty.Add(erpBOM.BITEMCODE,erpBOM.BQTY);
							}
						}

						foreach(object obj in htQty.Values)
						{
							if(Decimal.Parse(obj.ToString()) >= 0)
							{
								this.ShowMessage(new UserControl.Message(MessageType.Error,"$CS_ITEM_NOT_EXIST_ERPBOM"));

								//Application.DoEvents();
								ucLELotNo.TextFocus(true, true);
                                //Remove UCLabel.SelectAll;

								break;
							}
						}
					}
				
					CheckERPBOM(objERPBOMs);
				}
			}
		}

		private void txtBarcode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				//Laws Lu,2006/12/20 如果输入为空则直接返回
				if(txtBarcode.Value.Trim() == String.Empty)
				{
					//Application.DoEvents();
					txtBarcode.TextFocus(true, true);
					return;
				}

				string inputBarCode = txtBarcode.Value.Trim();
				//Laws Lu,2006/12/20 如果输入字符首字符串为"BR"则直接Return
				if(inputBarCode.Length > 2 && inputBarCode.Substring(0,2) == "BR")
				{
					//Application.DoEvents();
					txtBarcode.TextFocus(true, true);
					return;
				}
				//Laws Lu,2006/12/20 Item类型的条码
				/*▲▲▲ ○ ★★★★★ □ ●●● ☆☆☆☆☆☆ ﹡﹡﹡﹡﹡﹡
				 * ①   ②     ③     ④   ⑤        ⑥           ⑦
				 * ①----2码用数字或大写字母表示；
					②----固定字符1码用.点表示；
					③----5码用数字或大写字母表示；
					④----固定字符1码用.点表示；
					⑤----3码用数字或大写字母表示；
					⑥----日期6码，前2码表示为年份的后2位数字，如06表示2006年；中2码表示为月份，如果月份为1到9月份，中2码的第一位用0表示，如01表示1月份；后2码表示为日期，如果日期为1到9号，后2码的第一位用0表示，如01表示1号；
					⑦ ----6码表示数量，如果数量不够6码，前面用0代替，例如数量是200用000200表示。

				 * */
				#region Item解析逻辑
				int iFirstShow = inputBarCode.IndexOf(".");
				int iSecondShow = inputBarCode.LastIndexOf(".");
				if(iFirstShow == 2 && iSecondShow == 8)
				{
					//获取设置料号
					string itemCode = String.Empty;
					try
					{
						itemCode = inputBarCode.Substring(0,12);
					}
					catch(Exception ex)
					{
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));
						//Application.DoEvents();
						txtBarcode.TextFocus(true, true);
						return;
					}

					ucLEItemCode.Value = itemCode;
					//获取设置DateCode
					string dateCode = String.Empty;
					try
					{
						dateCode = inputBarCode.Substring(12,6);
					}
					catch(Exception ex)
					{
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));
						//Application.DoEvents();
						txtBarcode.TextFocus(true, true);
						return;
					}
					ucLEDateCode.Value = dateCode;
					
					//Application.DoEvents();
					txtBarcode.TextFocus(true, true);
					return;
				}
				#endregion

				//Laws Lu,2006/12/20 Item类型的条码
				/*▲▲▲▲▲ ○○○○○○○○○○○○○○○ ★★★
				 *   ①                    ②               ③    
				 * ①----5码表示厂商代码；
					②----15码表示厂商的Lot；
					③----3码表示流水号。 */
				#region Lot解析规则
				if(inputBarCode.Length == 23)
				{
					//厂商代码
					string supCode = String.Empty;
					try
					{
						supCode = inputBarCode.Substring(0,5);
					}
					catch(Exception ex)
					{
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));
						//Application.DoEvents();
						txtBarcode.TextFocus(true, true);
						return;
					}

					ucLEVendorCode.Value = supCode;
					//生产批号
					string lotNO = String.Empty;
					try
					{
						lotNO = inputBarCode.Substring(5,15);
					}
					catch(Exception ex)
					{
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));
						//Application.DoEvents();
						txtBarcode.TextFocus(true, true);
						return;
					}

					ucLELotNo.Value = lotNO;

					//Application.DoEvents();
					txtBarcode.TextFocus(true, true);
					return;
				}
				#endregion

				//Application.DoEvents();
				txtBarcode.TextFocus(true, true);
			}
		}
	}
}

