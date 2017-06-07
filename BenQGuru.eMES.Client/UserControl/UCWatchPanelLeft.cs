using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace UserControl
{
	/// <summary>
	/// UCWatchPanelLeft 的摘要说明。
	/// </summary>
	public class UCWatchPanelLeft : System.Windows.Forms.UserControl,IWatchPanel
	{
		private System.Windows.Forms.Panel pnlHeader;
		private System.Windows.Forms.Label lblKeyStation;
		private System.Windows.Forms.Label lblItem;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblResCode;
		private System.Windows.Forms.Panel pnlCenter;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label lblThruput;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label lblTotalActual;
		private System.Windows.Forms.Label lblGoal;
		private System.Windows.Forms.Panel pnlBottom;
		private System.Windows.Forms.Label lblActual;
		private System.Windows.Forms.Label lblFPYGoal;
		private System.Windows.Forms.Label lblDefects;
		private System.Windows.Forms.Label lblInput;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UCWatchPanelLeft()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
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
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.lblItem = new System.Windows.Forms.Label();
			this.lblKeyStation = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pnlBottom = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.lblTotalActual = new System.Windows.Forms.Label();
			this.lblGoal = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.lblThruput = new System.Windows.Forms.Label();
			this.pnlCenter = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.lblActual = new System.Windows.Forms.Label();
			this.lblFPYGoal = new System.Windows.Forms.Label();
			this.lblDefects = new System.Windows.Forms.Label();
			this.lblInput = new System.Windows.Forms.Label();
			this.lblResCode = new System.Windows.Forms.Label();
			this.pnlHeader.SuspendLayout();
			this.panel1.SuspendLayout();
			this.pnlBottom.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel2.SuspendLayout();
			this.pnlCenter.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlHeader
			// 
			this.pnlHeader.Controls.Add(this.lblItem);
			this.pnlHeader.Controls.Add(this.lblKeyStation);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(200, 23);
			this.pnlHeader.TabIndex = 0;
			// 
			// lblItem
			// 
			this.lblItem.BackColor = System.Drawing.Color.GreenYellow;
			this.lblItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblItem.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblItem.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
			this.lblItem.Location = new System.Drawing.Point(104, 0);
			this.lblItem.Name = "lblItem";
			this.lblItem.Size = new System.Drawing.Size(96, 23);
			this.lblItem.TabIndex = 1;
			this.lblItem.Text = "Item";
			this.lblItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblKeyStation
			// 
			this.lblKeyStation.BackColor = System.Drawing.Color.GreenYellow;
			this.lblKeyStation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblKeyStation.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblKeyStation.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
			this.lblKeyStation.Location = new System.Drawing.Point(0, 0);
			this.lblKeyStation.Name = "lblKeyStation";
			this.lblKeyStation.Size = new System.Drawing.Size(104, 23);
			this.lblKeyStation.TabIndex = 0;
			this.lblKeyStation.Text = "Key station";
			this.lblKeyStation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.pnlBottom);
			this.panel1.Controls.Add(this.pnlCenter);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 23);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 120);
			this.panel1.TabIndex = 1;
			// 
			// pnlBottom
			// 
			this.pnlBottom.Controls.Add(this.panel4);
			this.pnlBottom.Controls.Add(this.panel2);
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlBottom.Location = new System.Drawing.Point(0, 96);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size(200, 24);
			this.pnlBottom.TabIndex = 6;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.lblTotalActual);
			this.panel4.Controls.Add(this.lblGoal);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(104, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(96, 24);
			this.panel4.TabIndex = 1;
			// 
			// lblTotalActual
			// 
			this.lblTotalActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblTotalActual.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblTotalActual.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblTotalActual.Location = new System.Drawing.Point(0, 0);
			this.lblTotalActual.Name = "lblTotalActual";
			this.lblTotalActual.Size = new System.Drawing.Size(96, 24);
			this.lblTotalActual.TabIndex = 3;
			this.lblTotalActual.Text = "Total Actual";
			this.lblTotalActual.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblGoal
			// 
			this.lblGoal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblGoal.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblGoal.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblGoal.Location = new System.Drawing.Point(0, -6);
			this.lblGoal.Name = "lblGoal";
			this.lblGoal.Size = new System.Drawing.Size(96, 30);
			this.lblGoal.TabIndex = 2;
			this.lblGoal.Text = "Goal";
			this.lblGoal.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.lblGoal.Visible = false;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.lblThruput);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(104, 24);
			this.panel2.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.PaleTurquoise;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 30);
			this.label1.TabIndex = 1;
			this.label1.Visible = false;
			// 
			// lblThruput
			// 
			this.lblThruput.BackColor = System.Drawing.Color.PaleTurquoise;
			this.lblThruput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblThruput.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblThruput.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblThruput.Location = new System.Drawing.Point(0, 0);
			this.lblThruput.Name = "lblThruput";
			this.lblThruput.Size = new System.Drawing.Size(104, 24);
			this.lblThruput.TabIndex = 0;
			this.lblThruput.Text = "Thruput";
			this.lblThruput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlCenter
			// 
			this.pnlCenter.Controls.Add(this.panel3);
			this.pnlCenter.Controls.Add(this.lblResCode);
			this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlCenter.Location = new System.Drawing.Point(0, 0);
			this.pnlCenter.Name = "pnlCenter";
			this.pnlCenter.Size = new System.Drawing.Size(200, 96);
			this.pnlCenter.TabIndex = 5;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.lblActual);
			this.panel3.Controls.Add(this.lblFPYGoal);
			this.panel3.Controls.Add(this.lblDefects);
			this.panel3.Controls.Add(this.lblInput);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(104, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(96, 96);
			this.panel3.TabIndex = 1;
			// 
			// lblActual
			// 
			this.lblActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblActual.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblActual.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblActual.Location = new System.Drawing.Point(0, 72);
			this.lblActual.Name = "lblActual";
			this.lblActual.Size = new System.Drawing.Size(96, 24);
			this.lblActual.TabIndex = 1;
			this.lblActual.Text = "Actual";
			this.lblActual.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblFPYGoal
			// 
			this.lblFPYGoal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblFPYGoal.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblFPYGoal.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblFPYGoal.Location = new System.Drawing.Point(0, 48);
			this.lblFPYGoal.Name = "lblFPYGoal";
			this.lblFPYGoal.Size = new System.Drawing.Size(96, 24);
			this.lblFPYGoal.TabIndex = 2;
			this.lblFPYGoal.Text = "FPY Goal";
			this.lblFPYGoal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblDefects
			// 
			this.lblDefects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblDefects.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblDefects.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblDefects.Location = new System.Drawing.Point(0, 24);
			this.lblDefects.Name = "lblDefects";
			this.lblDefects.Size = new System.Drawing.Size(96, 24);
			this.lblDefects.TabIndex = 3;
			this.lblDefects.Text = "Defects";
			this.lblDefects.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblInput
			// 
			this.lblInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblInput.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblInput.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblInput.Location = new System.Drawing.Point(0, 0);
			this.lblInput.Name = "lblInput";
			this.lblInput.Size = new System.Drawing.Size(96, 24);
			this.lblInput.TabIndex = 4;
			this.lblInput.Text = "Input";
			this.lblInput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblResCode
			// 
			this.lblResCode.BackColor = System.Drawing.Color.PaleTurquoise;
			this.lblResCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblResCode.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblResCode.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblResCode.Location = new System.Drawing.Point(0, 0);
			this.lblResCode.Name = "lblResCode";
			this.lblResCode.Size = new System.Drawing.Size(104, 96);
			this.lblResCode.TabIndex = 0;
			this.lblResCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// UCWatchPanelLeft
			// 
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.pnlHeader);
			this.Name = "UCWatchPanelLeft";
			this.Size = new System.Drawing.Size(200, 143);
			this.pnlHeader.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.pnlBottom.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.pnlCenter.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void HideHeader()
		{
			this.pnlHeader.Visible = false;
		}

		public void HideBottom()
		{
			this.pnlBottom.Visible = false;
		}

		public int HeaderHeight
		{
			get
			{
				return this.pnlHeader.Height;
			}
		}

		public int BottomHeight
		{
			get
			{
				return this.pnlBottom.Height;
			}
		}

		public string ResCode
		{
			set
			{
				this.lblResCode.Text = value;
			}
			get
			{
				return this.lblResCode.Text;
			}
		}
	}
}
