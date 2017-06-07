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
	/// FSSChoose 的摘要说明。
	/// </summary>
	public class FSSChoose : System.Windows.Forms.Form
	{
		private UserControl.UCButton ucBtnExit;
		private UserControl.UCButton ucBtnOK;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		private string _moCode = string.Empty;
		private System.Windows.Forms.GroupBox groupBox1;
		private Infragistics.Win.UltraWinGrid.UltraGrid dataGrid;

		private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		public FSSChoose()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FSSChoose));
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Table1", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column1");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column2");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column3");
			this.ucBtnExit = new UserControl.UCButton();
			this.ucBtnOK = new UserControl.UCButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dataGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// ucBtnExit
			// 
			this.ucBtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
			this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
			this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
			this.ucBtnExit.Caption = "退出";
			this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucBtnExit.Location = new System.Drawing.Point(278, 353);
			this.ucBtnExit.Name = "ucBtnExit";
			this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
			this.ucBtnExit.TabIndex = 2;
			// 
			// ucBtnOK
			// 
			this.ucBtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ucBtnOK.BackColor = System.Drawing.SystemColors.Control;
			this.ucBtnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnOK.BackgroundImage")));
			this.ucBtnOK.ButtonType = UserControl.ButtonTypes.Confirm;
			this.ucBtnOK.Caption = "确认";
			this.ucBtnOK.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucBtnOK.Location = new System.Drawing.Point(134, 353);
			this.ucBtnOK.Name = "ucBtnOK";
			this.ucBtnOK.Size = new System.Drawing.Size(88, 22);
			this.ucBtnOK.TabIndex = 1;
			this.ucBtnOK.Click += new System.EventHandler(this.ucBtnOK_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.dataGrid);
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(634, 336);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "选择生产线";
			// 
			// dataGrid
			// 
			this.dataGrid.Cursor = System.Windows.Forms.Cursors.Default;
			ultraGridColumn1.Header.Caption = "生产线代码";
			ultraGridColumn1.Width = 150;
			ultraGridColumn2.Header.Caption = "生产线描述";
			ultraGridColumn2.Width = 200;
			ultraGridColumn3.Header.Caption = "所属工段";
			ultraGridColumn3.Width = 150;
			ultraGridBand1.Columns.Add(ultraGridColumn1);
			ultraGridBand1.Columns.Add(ultraGridColumn2);
			ultraGridBand1.Columns.Add(ultraGridColumn3);
			ultraGridBand1.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.dataGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid.Location = new System.Drawing.Point(3, 17);
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.Size = new System.Drawing.Size(628, 316);
			this.dataGrid.TabIndex = 7;
			// 
			// FSSChoose
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(643, 384);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ucBtnOK);
			this.Controls.Add(this.ucBtnExit);
			this.Name = "FSSChoose";
			this.Text = "产线选择";
			this.Load += new System.EventHandler(this.FSSChoose_Load);
			this.Closed += new System.EventHandler(this.FSSChoose_Closed);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		private void FSSChoose_Closed(object sender, System.EventArgs e)
		{
			if (this.DataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();  
		}

		private void ucBtnOK_Click(object sender, System.EventArgs e)
		{
			if(this.dataGrid.DisplayLayout.ActiveRow != null)
			{
				this.DialogResult = System.Windows.Forms.DialogResult.OK;
				this.Close();
			}
			else
			{
				MessageBox.Show("请选择一条记录");
			}
		}

		private void FSSChoose_Load(object sender, System.EventArgs e)
		{
			this.dataGrid.DataSource = LoadData();
			this.dataGrid.DataBind();
			for(int i=0;i<this.dataGrid.DisplayLayout.Bands[0].Columns.Count;i++)
			{
				this.dataGrid.DisplayLayout.Bands[0].Columns[i].Width = (int)(this.dataGrid.Width / this.dataGrid.DisplayLayout.Bands[0].Columns.Count);
			}
		}
	
		private System.Data.DataTable LoadData()
		{
			System.Data.DataTable dt = new System.Data.DataTable("SS");
			dt.Columns.Add("生产线代码");
			dt.Columns.Add("生产线描述");
			dt.Columns.Add("所属工段");
			//load 产线数据
			BenQGuru.eMES.BaseSetting.BaseModelFacade _baseFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
			object[] objs = _baseFacade.GetAllStepSequence();
			
			foreach(object obj in objs)
			{
				BenQGuru.eMES.Domain.BaseSetting.StepSequence ss = obj as BenQGuru.eMES.Domain.BaseSetting.StepSequence;
				if(ss != null)
				{
					dt.Rows.Add(new object[]{ss.StepSequenceCode,ss.StepSequenceDescription,ss.SegmentCode});
				}
			}
			return dt;
		}

		public string GetSSCode()
		{
			Infragistics.Win.UltraWinGrid.UltraGridRow row = this.dataGrid.DisplayLayout.ActiveRow;
			if(row != null)
			{
				return row.Cells[0].Value.ToString();
			}
			else
				return string.Empty;
		}
	}
}
