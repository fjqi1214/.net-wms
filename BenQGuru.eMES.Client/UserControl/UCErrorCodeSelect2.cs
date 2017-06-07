using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Domain.TS;

namespace UserControl
{
	
	public class UCErrorCodeSelect2 : System.Windows.Forms.UserControl
	{
        private Panel panelInput;
        private Panel panelGrid;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
        private UCLabelEdit txtInput;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UCErrorCodeSelect2()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();
            this.txtInput.InnerTextBox.KeyUp += new KeyEventHandler(InnerTextBox_KeyUp);
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

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Checked");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ErrorCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ErrorCodeDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Points");
            this.panelInput = new System.Windows.Forms.Panel();
            this.txtInput = new UserControl.UCLabelEdit();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panelInput.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelInput
            // 
            this.panelInput.Controls.Add(this.txtInput);
            this.panelInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelInput.Location = new System.Drawing.Point(0, 242);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(526, 38);
            this.panelInput.TabIndex = 0;
            // 
            // txtInput
            // 
            this.txtInput.AllowEditOnlyChecked = true;
            this.txtInput.Caption = "不良代码输入(&N)";
            this.txtInput.Checked = false;
            this.txtInput.EditType = UserControl.EditTypes.String;
            this.txtInput.Location = new System.Drawing.Point(8, 6);
            this.txtInput.MaxLength = 40;
            this.txtInput.Multiline = false;
            this.txtInput.Name = "txtInput";
            this.txtInput.PasswordChar = '\0';
            this.txtInput.ReadOnly = false;
            this.txtInput.ShowCheckBox = false;
            this.txtInput.Size = new System.Drawing.Size(503, 24);
            this.txtInput.TabIndex = 0;
            this.txtInput.TabNext = false;
            this.txtInput.Value = "";
            this.txtInput.WidthType = UserControl.WidthTypes.TooLong;
            this.txtInput.XAlign = 111;
            this.txtInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_TxtboxKeyPress);
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.ultraGrid1);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 0);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(526, 242);
            this.panelGrid.TabIndex = 1;
            // 
            // ultraGrid1
            // 
            this.ultraGrid1.Cursor = System.Windows.Forms.Cursors.Default;
            ultraGridColumn1.Header.Caption = "";
            ultraGridColumn1.Width = 30;
            ultraGridColumn2.Header.Caption = "不良代码";
            ultraGridColumn2.Width = 126;
            ultraGridColumn3.Header.Caption = "不良代码描述";
            ultraGridColumn3.Width = 212;
            ultraGridColumn4.Header.Caption = "不良位置(F5)";
            ultraGridColumn4.Width = 85;
            ultraGridColumn5.Header.Caption = "点数";
            ultraGridColumn5.Width = 37;
            ultraGridBand1.Columns.Add(ultraGridColumn1);
            ultraGridBand1.Columns.Add(ultraGridColumn2);
            ultraGridBand1.Columns.Add(ultraGridColumn3);
            ultraGridBand1.Columns.Add(ultraGridColumn4);
            ultraGridBand1.Columns.Add(ultraGridColumn5);
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid1.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.Size = new System.Drawing.Size(526, 242);
            this.ultraGrid1.TabIndex = 0;
            this.ultraGrid1.BeforeCellDeactivate += new System.ComponentModel.CancelEventHandler(this.ultraGrid1_BeforeCellDeactivate);
            this.ultraGrid1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ultraGrid1_KeyPress);
            // 
            // UCErrorCodeSelect2
            // 
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelInput);
            this.Name = "UCErrorCodeSelect2";
            this.Size = new System.Drawing.Size(526, 280);
            this.Load += new System.EventHandler(this.UCErrorCodeSelect2_Load);
            this.panelInput.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        private DataTable tblSource = null;
        private void UCErrorCodeSelect2_Load(object sender, EventArgs e)
        {
            InitUI();
        }

        private bool isShowPoint = false;
        private void InitUI()
        {
            this.ultraGrid1.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            this.ultraGrid1.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGrid1.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGrid1.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGrid1.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGrid1.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid1.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Blue;
            this.ultraGrid1.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.White;
            this.ultraGrid1.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");

            this.ultraGrid1.DisplayLayout.Bands[0].Columns["ErrorCode"].AutoEdit = false;
            this.ultraGrid1.DisplayLayout.Bands[0].Columns["ErrorCodeDescription"].AutoEdit = false;
            this.ultraGrid1.DisplayLayout.Bands[0].Columns["ErrorCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            this.ultraGrid1.DisplayLayout.Bands[0].Columns["ErrorCodeDescription"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            tblSource = new DataTable();
            tblSource.Columns.Add("Checked", typeof(bool));
            tblSource.Columns.Add("ErrorCode");
            tblSource.Columns.Add("ErrorCodeDescription");
            tblSource.Columns.Add("Location");
            tblSource.Columns.Add("Points");

            this.ultraGrid1.DataSource = tblSource;

            if (isShowPoint == true && this.ultraGrid1.DisplayLayout.Bands[0].Columns["Points"].Hidden == true)
            {
                this.ultraGrid1.DisplayLayout.Bands[0].Columns["Location"].Width = this.ultraGrid1.DisplayLayout.Bands[0].Columns["Location"].Width - this.ultraGrid1.DisplayLayout.Bands[0].Columns["Points"].Width;
            }
            else if (isShowPoint == false && this.ultraGrid1.DisplayLayout.Bands[0].Columns["Points"].Hidden == false)
            {
                this.ultraGrid1.DisplayLayout.Bands[0].Columns["Location"].Width = this.ultraGrid1.DisplayLayout.Bands[0].Columns["Location"].Width + this.ultraGrid1.DisplayLayout.Bands[0].Columns["Points"].Width;
            }
            this.ultraGrid1.DisplayLayout.Bands[0].Columns["Points"].Hidden = !isShowPoint;
            
        }

        public void ShowHidePointColumn(bool show)
        {
            this.isShowPoint = show;
        }

        public void FocusInput()
        {
            Application.DoEvents();
            this.txtInput.Focus();
        }

        /// <summary>
        /// 初始化不良代码列表
        /// </summary>
        /// <param name="errorCodes"></param>
        public void AddErrorCodes(ErrorCodeA[] errorCodes)
        {
            if (errorCodes == null)
                return;
            this.tblSource.Rows.Clear();
            for (int i = 0; i < errorCodes.Length; i++)
            {
                DataRow row = tblSource.NewRow();
                row["Checked"] = false;
                row["ErrorCode"] = errorCodes[i].ErrorCode;
                row["ErrorCodeDescription"] = errorCodes[i].ErrorDescription;
                row["Location"] = "";
                row["Points"] = "";
                tblSource.Rows.Add(row);
            }
        }

        /// <summary>
        /// 更新选择项
        /// </summary>
        /// <param name="errorCode2Loc"></param>
        public void AddSelectedErrorCodes(TSErrorCode2Location[] errorCode2Loc)
        {
            if (errorCode2Loc == null)
                return;
            for (int i = 0; i < errorCode2Loc.Length; i++)
            {
                DataRow[] rows = tblSource.Select("ErrorCode='" + errorCode2Loc[i].ErrorCode + "'");
                if (rows != null && rows.Length > 0)
                {
                    rows[0]["Checked"] = true;
                    rows[0]["Location"] = errorCode2Loc[i].ErrorLocation;
                    // Marked by Scott
                    //rows[0]["Points"] = errorCode2Loc[i].Points.ToString();
                }
            }
        }

        /// <summary>
        /// 返回勾选的不良代码
        /// </summary>
        /// <param name="errorCodes">不良代码</param>
        /// <param name="location">不良位置</param>
        /// <returns>数据总数</returns>
        public int SelectedItem(out string[] errorCodes, out string[] location)
        {
            string[] points;
            return SelectedItem(out errorCodes, out location, out points);
        }
        /// <summary>
        /// 返回勾选的不良代码
        /// </summary>
        /// <param name="errorCodes">不良代码</param>
        /// <param name="location">不良位置</param>
        /// <param name="points">点数</param>
        /// <returns>数据总数</returns>
        public int SelectedItem(out string[] errorCodes, out string[] location, out string[] points)
        {
            ArrayList listErrorCode = new ArrayList();
            ArrayList listLocation = new ArrayList();
            ArrayList listPoint = new ArrayList();
            for (int i = 0; i < tblSource.Rows.Count; i++)
            {
                if (Convert.ToBoolean(tblSource.Rows[i]["Checked"]) == true)
                {
                    listErrorCode.Add(tblSource.Rows[i]["ErrorCode"]);
                    listLocation.Add(tblSource.Rows[i]["Location"]);
                    listPoint.Add(tblSource.Rows[i]["Points"]);
                }
            }
            errorCodes = new string[listErrorCode.Count];
            listErrorCode.CopyTo(errorCodes);
            location = new string[listLocation.Count];
            listLocation.CopyTo(location);
            points = new string[listPoint.Count];
            listPoint.CopyTo(points);

            return listErrorCode.Count;
        }

        public void UpdateGridValue()
        {
            if (this.ultraGrid1.ActiveCell == null)
                return;
            string strText = this.ultraGrid1.Rows[this.ultraGrid1.ActiveCell.Row.Index].Cells["Location"].Text;
            this.tblSource.Rows[this.ultraGrid1.ActiveCell.Row.Index]["Location"] = strText;
            strText = this.ultraGrid1.Rows[this.ultraGrid1.ActiveCell.Row.Index].Cells["Checked"].Text;
            this.tblSource.Rows[this.ultraGrid1.ActiveCell.Row.Index]["Checked"] = strText;
            strText = this.ultraGrid1.Rows[this.ultraGrid1.ActiveCell.Row.Index].Cells["Points"].Text;
            this.tblSource.Rows[this.ultraGrid1.ActiveCell.Row.Index]["Points"] = strText;
        }

        private void InnerTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                if (this.ultraGrid1.ActiveRow != null &&
                    Convert.ToBoolean(tblSource.Rows[this.ultraGrid1.ActiveRow.Index]["Checked"]) == true)
                {
                    while (true)
                    {
                        this.ultraGrid1.ActiveCell = this.ultraGrid1.ActiveRow.Cells[this.ultraGrid1.DisplayLayout.Bands[0].Columns["Location"].Index];
                        Application.DoEvents();
                        if (this.ultraGrid1.ActiveCell != null)
                            break;
                    }
                    this.ultraGrid1.ActiveCell.Activate();
                    this.ultraGrid1.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
                    if (this.ultraGrid1.ActiveCell != null && this.ultraGrid1.ActiveCell.Text != "")
                    {
                        this.ultraGrid1.ActiveCell.SelStart = 0;
                        this.ultraGrid1.ActiveCell.SelLength = this.ultraGrid1.ActiveCell.Text.Length;
                    }
                }
                return;
            }

            if (this.txtInput.Value.Trim() == "")
                return;
            if (this.ultraGrid1.Rows.Count == 0)
                return;
            if (this.ultraGrid1.ActiveRow != null)
            {
                if (this.ultraGrid1.ActiveRow.Cells["ErrorCode"].Value.ToString().ToUpper() == this.txtInput.Value.Trim().ToUpper())
                    return;
            }

            string strInput = this.txtInput.Value.Trim().ToUpper();
            int iLastMatchIdx = -1;
            for (int i = 0; i < this.ultraGrid1.Rows.Count; i++)
            {
                string strErrCode = this.ultraGrid1.Rows[i].Cells["ErrorCode"].Value.ToString().ToUpper();
                if (strErrCode.Length >= strInput.Length)
                {
                    if (strErrCode == strInput)
                    {
                        iLastMatchIdx = i;
                        break;
                    }
                    else if (strErrCode.Substring(0, strInput.Length) == strInput)
                    {
                        if (iLastMatchIdx == -1)
                            iLastMatchIdx = i;
                    }
                }
            }
            if (iLastMatchIdx == -1)
                iLastMatchIdx = 0;
            this.ultraGrid1.Rows[iLastMatchIdx].Activate();
        }

        private void txtInput_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && this.ultraGrid1.ActiveRow != null)
            {
                if (this.ultraGrid1.ActiveRow.Cells["ErrorCode"].Value.ToString().ToUpper() == this.txtInput.Value.Trim().ToUpper())
                {
                    tblSource.Rows[this.ultraGrid1.ActiveRow.Index]["Checked"] = true;
                    this.txtInput.Value = "";
                    
                    this.ultraGrid1.ActiveCell = this.ultraGrid1.ActiveRow.Cells[this.ultraGrid1.DisplayLayout.Bands[0].Columns["Location"].Index];
                    this.ultraGrid1.ActiveCell.Activate();
                    this.ultraGrid1.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
                    if (this.ultraGrid1.ActiveCell != null && this.ultraGrid1.ActiveCell.Text != "")
                    {
                        this.ultraGrid1.ActiveCell.SelStart = 0;
                        this.ultraGrid1.ActiveCell.SelLength = this.ultraGrid1.ActiveCell.Text.Length;
                    }
                }
            }
        }

        private void ultraGrid1_BeforeCellDeactivate(object sender, CancelEventArgs e)
        {
        }

        private void ultraGrid1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Application.DoEvents();
                this.txtInput.Focus();
                this.txtInput.SelectAll();
            }
        }

    }
}
