using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BenQGuru.eMES.Client
{
	public class FCartonSplit : Form
	{
		private System.Windows.Forms.Panel panel1;
		private UserControl.UCLabelEdit ucLabEdit1;
		private System.Windows.Forms.GroupBox groupBox1;
		private UserControl.UCButton ucButton2;
		private UserControl.UCLabelEdit ucLabEdit5;
		private UserControl.UCButton ucButton4;
		private Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSet3;
		private Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSet1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
		private System.Data.DataSet dataSet1;
		private System.Data.DataTable dataTable1;
		private System.Data.DataColumn dataColumn1;
		private System.Data.DataColumn dataColumn2;
		private System.Data.DataColumn dataColumn3;
		private System.Data.DataColumn dataColumn4;
		private System.ComponentModel.IContainer components = null;

		public FCartonSplit()
		{
			// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化
			UserControl.UIStyleBuilder.FormUI(this);	
			UserControl.UIStyleBuilder.GridUI(this.ultraGrid1);
			UserControl.UIStyleBuilder.UltraOptionSetUI(this.ultraOptionSet1);
			UserControl.UIStyleBuilder.UltraOptionSetUI(this.ultraOptionSet3);
			dataSet1.Tables[0].Rows.Add(new object[]{
														"001","MO001","Item1","LINE01"
													});
			dataSet1.Tables[0].Rows.Add(new object[]{
														"001","MO001","Item2","LINE01"
													});
		}
		protected void ShowMessage(string message)
		{
			///lablastMsg.Text =message;
			MessageBox.Show(message);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FCartonSplit));
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Table1", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column1");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column2");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column3");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Column4");
			this.panel1 = new System.Windows.Forms.Panel();
			this.ucLabEdit1 = new UserControl.UCLabelEdit();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ucButton2 = new UserControl.UCButton();
			this.ucButton4 = new UserControl.UCButton();
			this.ucLabEdit5 = new UserControl.UCLabelEdit();
			this.ultraOptionSet1 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
			this.ultraOptionSet3 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.dataTable1 = new System.Data.DataTable();
			this.dataColumn1 = new System.Data.DataColumn();
			this.dataColumn2 = new System.Data.DataColumn();
			this.dataColumn3 = new System.Data.DataColumn();
			this.dataColumn4 = new System.Data.DataColumn();
			this.dataSet1 = new System.Data.DataSet();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ultraOptionSet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraOptionSet3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.ucLabEdit1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(480, 40);
			this.panel1.TabIndex = 3;
			// 
			// ucLabEdit1
			// 
			this.ucLabEdit1.AllowEditOnlyChecked = true;
			this.ucLabEdit1.Caption = "包装号";
			this.ucLabEdit1.Checked = false;
			this.ucLabEdit1.Location = new System.Drawing.Point(8, 8);
			this.ucLabEdit1.Name = "ucLabEdit1";
			this.ucLabEdit1.ReadOnly = false;
			this.ucLabEdit1.ShowCheckBox = false;
			this.ucLabEdit1.Size = new System.Drawing.Size(183, 24);
			this.ucLabEdit1.TabIndex = 0;
			this.ucLabEdit1.Value = "";
			this.ucLabEdit1.WidthType = UserControl.WidthTypes.Normal;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ucButton2);
			this.groupBox1.Controls.Add(this.ucButton4);
			this.groupBox1.Controls.Add(this.ucLabEdit5);
			this.groupBox1.Controls.Add(this.ultraOptionSet1);
			this.groupBox1.Controls.Add(this.ultraOptionSet3);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 279);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(480, 78);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			// 
			// ucButton2
			// 
			this.ucButton2.BackColor = System.Drawing.SystemColors.Control;
			this.ucButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton2.BackgroundImage")));
			this.ucButton2.ButtonType = UserControl.ButtonTypes.Confirm;
			this.ucButton2.Caption = "确认";
			this.ucButton2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucButton2.Location = new System.Drawing.Point(248, 48);
			this.ucButton2.Name = "ucButton2";
			this.ucButton2.Size = new System.Drawing.Size(88, 22);
			this.ucButton2.TabIndex = 0;
			// 
			// ucButton4
			// 
			this.ucButton4.BackColor = System.Drawing.SystemColors.Control;
			this.ucButton4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton4.BackgroundImage")));
			this.ucButton4.ButtonType = UserControl.ButtonTypes.Exit;
			this.ucButton4.Caption = "退出";
			this.ucButton4.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucButton4.Location = new System.Drawing.Point(352, 48);
			this.ucButton4.Name = "ucButton4";
			this.ucButton4.Size = new System.Drawing.Size(88, 22);
			this.ucButton4.TabIndex = 0;
			// 
			// ucLabEdit5
			// 
			this.ucLabEdit5.AllowEditOnlyChecked = true;
			this.ucLabEdit5.Caption = "输入框";
			this.ucLabEdit5.Checked = false;
			this.ucLabEdit5.Location = new System.Drawing.Point(24, 48);
			this.ucLabEdit5.Name = "ucLabEdit5";
			this.ucLabEdit5.ReadOnly = false;
			this.ucLabEdit5.ShowCheckBox = false;
			this.ucLabEdit5.Size = new System.Drawing.Size(183, 24);
			this.ucLabEdit5.TabIndex = 0;
			this.ucLabEdit5.Value = "";
			this.ucLabEdit5.WidthType = UserControl.WidthTypes.Normal;
			this.ucLabEdit5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabEdit5_KeyPress);
			// 
			// ultraOptionSet1
			// 
			this.ultraOptionSet1.CheckedIndex = 0;
			this.ultraOptionSet1.ItemAppearance = appearance1;
			valueListItem1.DataValue = "ValueListItem0";
			valueListItem1.DisplayText = "拆箱";
			valueListItem2.DataValue = "ValueListItem1";
			valueListItem2.DisplayText = "并箱";
			this.ultraOptionSet1.Items.Add(valueListItem1);
			this.ultraOptionSet1.Items.Add(valueListItem2);
			this.ultraOptionSet1.Location = new System.Drawing.Point(192, 19);
			this.ultraOptionSet1.Name = "ultraOptionSet1";
			this.ultraOptionSet1.Size = new System.Drawing.Size(144, 24);
			this.ultraOptionSet1.TabIndex = 0;
			this.ultraOptionSet1.Text = "拆箱";
			// 
			// ultraOptionSet3
			// 
			this.ultraOptionSet3.CheckedIndex = 0;
			this.ultraOptionSet3.ItemAppearance = appearance2;
			valueListItem3.DataValue = "ValueListItem0";
			valueListItem3.DisplayText = "Carton";
			valueListItem4.DataValue = "ValueListItem1";
			valueListItem4.DisplayText = "Pallet";
			this.ultraOptionSet3.Items.Add(valueListItem3);
			this.ultraOptionSet3.Items.Add(valueListItem4);
			this.ultraOptionSet3.Location = new System.Drawing.Point(32, 19);
			this.ultraOptionSet3.Name = "ultraOptionSet3";
			this.ultraOptionSet3.Size = new System.Drawing.Size(144, 24);
			this.ultraOptionSet3.TabIndex = 0;
			this.ultraOptionSet3.Text = "Carton";
			// 
			// listBox1
			// 
			this.listBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(0, 191);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(480, 88);
			this.listBox1.TabIndex = 1;
			// 
			// ultraGrid1
			// 
			this.ultraGrid1.Cursor = System.Windows.Forms.Cursors.Default;
			this.ultraGrid1.DataSource = this.dataTable1;
			ultraGridColumn1.Header.Caption = "产品序列号";
			ultraGridColumn2.Header.Caption = "工单";
			ultraGridColumn3.Header.Caption = "料号";
			ultraGridColumn4.Header.Caption = "产线";
			ultraGridBand1.Columns.Add(ultraGridColumn1);
			ultraGridBand1.Columns.Add(ultraGridColumn2);
			ultraGridBand1.Columns.Add(ultraGridColumn3);
			ultraGridBand1.Columns.Add(ultraGridColumn4);
			this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			this.ultraGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ultraGrid1.Location = new System.Drawing.Point(0, 40);
			this.ultraGrid1.Name = "ultraGrid1";
			this.ultraGrid1.Size = new System.Drawing.Size(480, 151);
			this.ultraGrid1.TabIndex = 4;
			// 
			// dataTable1
			// 
			this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
																			  this.dataColumn1,
																			  this.dataColumn2,
																			  this.dataColumn3,
																			  this.dataColumn4});
			this.dataTable1.TableName = "Table1";
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
			// dataSet1
			// 
			this.dataSet1.DataSetName = "NewDataSet";
			this.dataSet1.Locale = new System.Globalization.CultureInfo("zh-CN");
			this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
																		  this.dataTable1});
			// 
			// FCartonSplit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(480, 357);
			this.Controls.Add(this.ultraGrid1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panel1);
			this.Name = "FCartonSplit";
			this.Text = "包装拆并";
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ultraOptionSet1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraOptionSet3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
		
            ShowMessage("保存成功！");		
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			
		}

		private void txtErrorCode_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar==(char)13	)
			{
				btnAdd_Click(null,null);
			}
		
		}

		private void btnCancle_Click(object sender, System.EventArgs e)
		{
			
			ShowMessage("保存成功！");	
		
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void ucLabEdit5_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			/*if (e.KeyChar =='x')
			{
				ucButton2.Visible =!ucButton2.Visible ;
				ucButton3.Visible =!ucButton3.Visible ;
			}*/
		}
	}
}

