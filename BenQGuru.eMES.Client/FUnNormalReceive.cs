#region using
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using UserControl;
using BenQGuru.eMES.OQC;
#endregion

namespace BenQGuru.eMES.Client
{
	public class FUnNormalReceive : BenQGuru.eMES.Client.FNormalReceive
	{
		public FUnNormalReceive()
		{
		// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化
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
            Infragistics.Win.ValueListItem valueListItem11 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem12 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem9 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem10 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("*");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SEQ");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MoCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ItemCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ItemDesc");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PlanQty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ActQty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("status");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinScrollBar.ScrollBarLook scrollBarLook1 = new Infragistics.Win.UltraWinScrollBar.ScrollBarLook();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            ((System.ComponentModel.ISupportInitialize)(this._tmpTable)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOsType)).BeginInit();
            this.pnlAbnormal.SuspendLayout();
            this.panelReceiveNo.SuspendLayout();
            this.panelDataInput.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelDetail.SuspendLayout();
            this.gbxDetail.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridContent)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.Location = new System.Drawing.Point(422, 12);
            // 
            // ucBtnComplete
            // 
            this.ucBtnComplete.Location = new System.Drawing.Point(232, 12);
            // 
            // groupBox1
            // 
            this.groupBox1.Size = new System.Drawing.Size(832, 45);
            // 
            // ultraOsType
            // 
            this.ultraOsType.CheckedIndex = 0;
            valueListItem11.DataValue = "Carton";
            valueListItem11.DisplayText = "Carton";
            valueListItem12.DataValue = "PCS";
            valueListItem12.DisplayText = "PCS";
            valueListItem9.DataValue = "Carton";
            valueListItem9.DisplayText = "Carton";
            valueListItem10.DataValue = "PCS";
            valueListItem10.DisplayText = "PCS";
            valueListItem7.DataValue = "Carton";
            valueListItem7.DisplayText = "Carton";
            valueListItem8.DataValue = "PCS";
            valueListItem8.DisplayText = "PCS";
            valueListItem5.DataValue = "Carton";
            valueListItem5.DisplayText = "Carton";
            valueListItem6.DataValue = "PCS";
            valueListItem6.DisplayText = "PCS";
            valueListItem1.DataValue = "Carton";
            valueListItem1.DisplayText = "Carton";
            valueListItem2.DataValue = "PCS";
            valueListItem2.DisplayText = "PCS";
            valueListItem3.DataValue = "planarcode";
            valueListItem3.DisplayText = "Carton";
            valueListItem4.DataValue = "pcs";
            valueListItem4.DisplayText = "PCS";
            this.ultraOsType.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem11,
            valueListItem12,
            valueListItem9,
            valueListItem10,
            valueListItem7,
            valueListItem8,
            valueListItem5,
            valueListItem6,
            valueListItem1,
            valueListItem2,
            valueListItem3,
            valueListItem4});
            this.ultraOsType.Size = new System.Drawing.Size(554, 26);
            this.ultraOsType.Text = "Carton";
            // 
            // txtSumNum
            // 
            this.txtSumNum.Location = new System.Drawing.Point(236, 7);
            this.txtSumNum.Size = new System.Drawing.Size(158, 21);
            this.txtSumNum.XAlign = 294;
            // 
            // txtItemDesc
            // 
            this.txtItemDesc.Location = new System.Drawing.Point(435, 6);
            this.txtItemDesc.Size = new System.Drawing.Size(188, 22);
            this.txtItemDesc.XAlign = 504;
            // 
            // cbxItemCode
            // 
            this.cbxItemCode.Location = new System.Drawing.Point(235, 6);
            this.cbxItemCode.XAlign = 292;
            // 
            // cbxModel
            // 
            this.cbxModel.Location = new System.Drawing.Point(24, 6);
            this.cbxModel.Size = new System.Drawing.Size(180, 19);
            this.cbxModel.XAlign = 71;
            // 
            // pnlAbnormal
            // 
            this.pnlAbnormal.Location = new System.Drawing.Point(0, 66);
            this.pnlAbnormal.Size = new System.Drawing.Size(738, 28);
            // 
            // panelReceiveNo
            // 
            this.panelReceiveNo.Location = new System.Drawing.Point(0, 45);
            this.panelReceiveNo.Size = new System.Drawing.Size(832, 41);
            // 
            // panelDataInput
            // 
            this.panelDataInput.Location = new System.Drawing.Point(0, 86);
            this.panelDataInput.Size = new System.Drawing.Size(832, 36);
            // 
            // btnDeleteDetail
            // 
            this.btnDeleteDetail.Location = new System.Drawing.Point(6, 6);
            // 
            // btnDeleteRCard
            // 
            this.btnDeleteRCard.Location = new System.Drawing.Point(724, 7);
            // 
            // panelBottom
            // 
            this.panelBottom.Location = new System.Drawing.Point(0, 438);
            this.panelBottom.Size = new System.Drawing.Size(832, 71);
            // 
            // panelDetail
            // 
            this.panelDetail.Location = new System.Drawing.Point(0, 122);
            this.panelDetail.Size = new System.Drawing.Size(832, 316);
            // 
            // gbxDetail
            // 
            this.gbxDetail.Size = new System.Drawing.Size(832, 316);
            // 
            // panel2
            // 
            this.panel2.Size = new System.Drawing.Size(826, 296);
            // 
            // panelGrid
            // 
            this.panelGrid.Size = new System.Drawing.Size(826, 296);
            // 
            // ultraGridContent
            // 
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ultraGridContent.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 20;
            ultraGridColumn2.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn2.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn2.Width = 100;
            ultraGridColumn3.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn3.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn3.Width = 100;
            ultraGridColumn4.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn4.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn4.Header.Caption = "工单";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 100;
            ultraGridColumn5.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn5.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn5.Header.Caption = "产品别";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 100;
            ultraGridColumn6.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn6.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn6.Header.Caption = "产品代码";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 100;
            ultraGridColumn7.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn7.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn7.Header.Caption = "产品描述";
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Width = 100;
            ultraGridColumn8.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn8.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn8.Header.Caption = "计划数量";
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Width = 100;
            ultraGridColumn9.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn9.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn9.Header.Caption = "实际数量";
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Width = 100;
            ultraGridColumn10.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn10.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn10.Width = 100;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10});
            this.ultraGridContent.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ultraGridContent.DisplayLayout.CaptionAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.Gainsboro;
            appearance3.ForeColor = System.Drawing.Color.Black;
            this.ultraGridContent.DisplayLayout.Override.ActiveRowAppearance = appearance3;
            this.ultraGridContent.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
            appearance4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ultraGridContent.DisplayLayout.Override.HeaderAppearance = appearance4;
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ultraGridContent.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(245)))));
            this.ultraGridContent.DisplayLayout.Override.RowAppearance = appearance6;
            this.ultraGridContent.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridContent.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed;
            this.ultraGridContent.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGridContent.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            appearance7.BackColor = System.Drawing.Color.LightGray;
            scrollBarLook1.Appearance = appearance7;
            this.ultraGridContent.DisplayLayout.ScrollBarLook = scrollBarLook1;
            this.ultraGridContent.Size = new System.Drawing.Size(826, 296);
            // 
            // ucLEInput
            // 
            this.ucLEInput.Size = new System.Drawing.Size(200, 20);
            // 
            // btnUnComplete
            // 
            this.btnUnComplete.Location = new System.Drawing.Point(327, 12);
            // 
            // txtCartonNum
            // 
            this.txtCartonNum.Location = new System.Drawing.Point(446, 7);
            this.txtCartonNum.Size = new System.Drawing.Size(158, 21);
            this.txtCartonNum.XAlign = 504;
            // 
            // ucLETicketNo
            // 
            this.ucLETicketNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ucLETicketNo.Location = new System.Drawing.Point(75, 7);
            this.ucLETicketNo.MaxLength = 10000;
            this.ucLETicketNo.Size = new System.Drawing.Size(145, 20);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(548, 0);
            this.panel1.Size = new System.Drawing.Size(284, 36);
            // 
            // rbMO
            // 
            this.rbMO.Location = new System.Drawing.Point(97, 9);
            this.rbMO.Size = new System.Drawing.Size(87, 22);
            // 
            // rbProduct
            // 
            this.rbProduct.Location = new System.Drawing.Point(190, 9);
            this.rbProduct.Size = new System.Drawing.Size(86, 22);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 11);
            this.label2.Size = new System.Drawing.Size(85, 14);
            // 
            // FUnNormalReceive
            // 
            this.ClientSize = new System.Drawing.Size(832, 509);
            this.Name = "FUnNormalReceive";
            this.Text = "非生产入库采集";
            ((System.ComponentModel.ISupportInitialize)(this._tmpTable)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraOsType)).EndInit();
            this.pnlAbnormal.ResumeLayout(false);
            this.panelReceiveNo.ResumeLayout(false);
            this.panelReceiveNo.PerformLayout();
            this.panelDataInput.ResumeLayout(false);
            this.panelDataInput.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelDetail.ResumeLayout(false);
            this.gbxDetail.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridContent)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		protected override void InitControl()
		{
			this.pnlAbnormal.Show();
			this.ultraGridContent.DisplayLayout.Bands[0].Columns["MoCode"].Hidden = true;
			//this.gridRCard.DisplayLayout.Bands[0].Columns["MoCode"].Hidden = true;
			this.gbxDetail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Visible = false;
			this.rbMO.Visible = false;
			this.rbProduct.Visible = false;
		}
		
		protected override void BindModel()
		{
			int i = this.cbxModel.ComboBoxData.SelectedIndex;

			this.cbxModel.ComboBoxData.Items.Clear();
			object[] objs = this._facade.QueryInvReceive(this.ucLETicketNo.Text.Trim());
			if(objs != null)
			{
				foreach(object obj in objs)
				{
					InvReceive rec = obj as InvReceive;
					if(rec != null)
					{
						this.cbxModel.ComboBoxData.Items.Add(rec.ModelCode);
					}
				}
			}
			if(this.cbxModel.ComboBoxData.Items.Count > 0)
			{
				if( i >=0 && i<this.cbxModel.ComboBoxData.Items.Count)
					this.cbxModel.ComboBoxData.SelectedIndex = i;
				else
					this.cbxModel.ComboBoxData.SelectedIndex = 0;
			}
		}

		protected override void BindItemCode()
		{
			int i = this.cbxItemCode.ComboBoxData.SelectedIndex;

			this.cbxItemCode.ComboBoxData.Items.Clear();
			if(this.cbxModel.ComboBoxData.SelectedIndex >= 0 && this.cbxModel.ComboBoxData.SelectedItem != null)
			{
				object[] objs = this._facade.QueryInvReceive(this.ucLETicketNo.Text.Trim(),
															this.cbxModel.ComboBoxData.SelectedItem.ToString());
				if(objs != null)
				{
					foreach(object obj in objs)
					{
						InvReceive rec = obj as InvReceive;
						if(rec != null)
						{
							this.cbxItemCode.ComboBoxData.Items.Add(rec.ItemCode);
						}
					}
				}	
			}
			if(this.cbxItemCode.ComboBoxData.Items.Count > 0)
			{
				if( i >= 0 && i< this.cbxItemCode.ComboBoxData.Items.Count)
				{
					this.cbxItemCode.ComboBoxData.SelectedIndex = i;
				}
				else
				{
					this.cbxItemCode.ComboBoxData.SelectedIndex = 0;
				}
			}
		}

		private void ActivateRowByItem(string item)
		{
//			foreach(Infragistics.Win.UltraWinGrid.UltraGridRow row in this.ultraGridContent.Rows)
			for ( int iGridRowLoopIndex = 0; iGridRowLoopIndex < this.ultraGridContent.Rows.Count; iGridRowLoopIndex++)
			{
				Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridContent.Rows[iGridRowLoopIndex];
				if(row.Cells["ItemCode"].Text == item)
				{
					if(!row.Activated)
						row.Activated = true;
				}
				else
				{
					row.Activated = false;
					row.Selected = false;
				}
			}
		}

		protected override void SetItemDesc()
		{
			if(this.cbxItemCode.ComboBoxData.SelectedItem != null)
			{
				InvReceive rec = _facade.GetInvReceive(this.ucLETicketNo.Text.Trim(),this.cbxItemCode.ComboBoxData.SelectedItem.ToString());
				if(rec != null)
				{
					this.txtItemDesc.InnerTextBox.Text = rec.ItemDesc;
				}

				this.ActivateRowByItem(this.cbxItemCode.ComboBoxData.SelectedItem.ToString());

//				ItemFacade _facade = new ItemFacade(this.DataProvider);
//				BenQGuru.eMES.Domain.MOModel.Item item =  _facade.GetItem(this.cbxItemCode.ComboBoxData.SelectedItem.ToString()) as BenQGuru.eMES.Domain.MOModel.Item;
//				if(item != null)
//				{
//					this.txtItemDesc.InnerTextBox.Text = item.ItemDescription;
//				}
			}
		}

		protected override bool CheckModel(BarCodeParse barParser)
		{
			string model = barParser.GetModelCode( this.ucLEInput.Text.Trim() );

			if(this.cbxModel.ComboBoxData.SelectedItem != null && this.cbxModel.ComboBoxData.SelectedItem.ToString() != model)
			{
				this.ErrorMessage("产品别选择错误");
				return false;
			}
			else if(this.cbxModel.ComboBoxData.SelectedItem == null)
			{
				this.ErrorMessage("请选择产品别");
				return false;
			}

			return true;
		}

		protected override SimulateResult GetItemList(string rcard)
		{
			SimulateResult sr = new SimulateResult();
			if(this.cbxItemCode.ComboBoxData.SelectedItem == null)
				throw new Exception("请选择产品代码");


			sr.ItemCode = this.cbxItemCode.ComboBoxData.SelectedItem.ToString();
			sr.MOCode = string.Empty;
			sr.IsCompleted = true;
			sr.IsInv = false;
			sr.RunningCard = rcard;

			return sr;
		}
		
		protected override DataRow[] GetDetailGridRow(InvReceive rec)
		{
			return _tmpTable.Select(string.Format("ModelCode='{0}' and ItemCode='{1}'",rec.ModelCode,rec.ItemCode));
		}

		protected override DataRow[] GetDetailGridRow(string recno,SimulateResult sr)
		{
			return _tmpTable.Select(string.Format("NO='{0}' and ItemCode='{1}'",recno,sr.ItemCode));
		}

		protected override void CheckMoOrItem(InvReceive rec,SimulateResult sr)
		{
			if(rec == null)
				throw new Exception(sr.RunningCard +" $Error_No_Inv_Detail");//没有找到可用的入库单明细
		}
	}
}

