using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.DataCollect;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.MOModel;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FfunTestInfo 的摘要说明。
	/// </summary>
	public class FfunTestInfo : System.Windows.Forms.Form
	{
		private string _lotno;
		private DataTable dtIDList = new DataTable();

		private UserControl.UCLabelEdit txtFunTestQty;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
		private UserControl.UCButton btnExit;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FfunTestInfo()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			UserControl.UIStyleBuilder.FormUI(this);	
			UserControl.UIStyleBuilder.GridUI(ultraGridMain);	

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		public FfunTestInfo(string lotno)
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			UserControl.UIStyleBuilder.FormUI(this);	
			UserControl.UIStyleBuilder.GridUI(ultraGridMain);	

			_lotno = lotno;
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

		private void InitializeGrid()
		{
			dtIDList.Columns.Clear();
			dtIDList.Columns.Add("runningcard",typeof(string)).ReadOnly = true;
			dtIDList.Columns.Add("result",typeof(string)).ReadOnly = true;

			this.ultraGridMain.DataSource = dtIDList;
		}

		private void ultraGridMain_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
		{
			UltraWinGridHelper ultraWinGridHelper = new UltraWinGridHelper(this.ultraGridMain);
			ultraWinGridHelper.AddCommonColumn("runningcard","产品序列号");
			ultraWinGridHelper.AddCommonColumn("result","测试结果");

			//			foreach(UltraGridBand ub in ultraGridMain.co
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FfunTestInfo));
			this.txtFunTestQty = new UserControl.UCLabelEdit();
			this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.btnExit = new UserControl.UCButton();
			((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
			this.SuspendLayout();
			// 
			// txtFunTestQty
			// 
			this.txtFunTestQty.AllowEditOnlyChecked = true;
			this.txtFunTestQty.Caption = "功能测试数量";
			this.txtFunTestQty.Checked = false;
			this.txtFunTestQty.EditType = UserControl.EditTypes.Integer;
			this.txtFunTestQty.Location = new System.Drawing.Point(24, 16);
			this.txtFunTestQty.MaxLength = 40;
			this.txtFunTestQty.Multiline = false;
			this.txtFunTestQty.Name = "txtFunTestQty";
			this.txtFunTestQty.PasswordChar = '\0';
			this.txtFunTestQty.ReadOnly = true;
			this.txtFunTestQty.ShowCheckBox = false;
			this.txtFunTestQty.Size = new System.Drawing.Size(220, 24);
			this.txtFunTestQty.TabIndex = 1;
			this.txtFunTestQty.TabNext = true;
			this.txtFunTestQty.Value = "";
			this.txtFunTestQty.WidthType = UserControl.WidthTypes.Normal;
			this.txtFunTestQty.XAlign = 111;
			// 
			// ultraGridMain
			// 
			this.ultraGridMain.Cursor = System.Windows.Forms.Cursors.Default;
			this.ultraGridMain.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.ultraGridMain.Location = new System.Drawing.Point(16, 48);
			this.ultraGridMain.Name = "ultraGridMain";
			this.ultraGridMain.Size = new System.Drawing.Size(464, 240);
			this.ultraGridMain.TabIndex = 8;
			this.ultraGridMain.Text = "功能测试产品";
			this.ultraGridMain.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMain_InitializeLayout);
			// 
			// btnExit
			// 
			this.btnExit.BackColor = System.Drawing.SystemColors.Control;
			this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
			this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
			this.btnExit.Caption = "退出";
			this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnExit.Location = new System.Drawing.Point(384, 16);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(88, 22);
			this.btnExit.TabIndex = 10;
			// 
			// FfunTestInfo
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(496, 293);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.ultraGridMain);
			this.Controls.Add(this.txtFunTestQty);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FfunTestInfo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "功能测试不良信息";
			this.Load += new System.EventHandler(this.FfunTestInfo_Load);
			((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		private void FfunTestInfo_Load(object sender, System.EventArgs e)
		{
			InitializeGrid();
			LoadData();
		}

		private void LoadData()
		{
			OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
			object[] objs = oqcFacade.GetOQCFuncTestValueByLotNo(this._lotno, OQCFacade.Lot_Sequence_Default);
			if (objs != null)
			{
				for (int i = 0; i < objs.Length; i++)
				{
					OQCFuncTestValue testValue = (OQCFuncTestValue)objs[i];
					DataRow row = dtIDList.NewRow();
					row["runningcard"] = testValue.RunningCard;
					row["result"] = testValue.ProductStatus;
					dtIDList.Rows.Add(row);
				}
				this.txtFunTestQty.Value = objs.Length.ToString();
			}
			else
			{
				this.txtFunTestQty.Value = "0";
			}
		}

	}
}
