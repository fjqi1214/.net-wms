using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace UserControl
{
	/// <summary>
	/// UCWatchPanelContent 的摘要说明。
	/// </summary>
	public class UCWatchPanelCenter : System.Windows.Forms.UserControl,ICalculateWatchPanel
	{
		private System.Windows.Forms.Panel pnlHeader;
		private System.Windows.Forms.Label lblTimePeriod;
		private System.Windows.Forms.TextBox txtInput;
		private System.Windows.Forms.TextBox txtDefects;
		private System.Windows.Forms.TextBox txtFPYGoal;
		private System.Windows.Forms.TextBox txtActual;
		private System.Windows.Forms.Panel pnlCenter;
		private System.Windows.Forms.Panel pnlFill;
		private System.Windows.Forms.Panel pnlBottom;
		private System.Windows.Forms.TextBox txtTotalActual;
		private System.Windows.Forms.TextBox txtGoal;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UCWatchPanelCenter()
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
			this.lblTimePeriod = new System.Windows.Forms.Label();
			this.txtInput = new System.Windows.Forms.TextBox();
			this.txtDefects = new System.Windows.Forms.TextBox();
			this.txtFPYGoal = new System.Windows.Forms.TextBox();
			this.txtActual = new System.Windows.Forms.TextBox();
			this.pnlCenter = new System.Windows.Forms.Panel();
			this.pnlFill = new System.Windows.Forms.Panel();
			this.pnlBottom = new System.Windows.Forms.Panel();
			this.txtTotalActual = new System.Windows.Forms.TextBox();
			this.txtGoal = new System.Windows.Forms.TextBox();
			this.pnlHeader.SuspendLayout();
			this.pnlCenter.SuspendLayout();
			this.pnlFill.SuspendLayout();
			this.pnlBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlHeader
			// 
			this.pnlHeader.Controls.Add(this.lblTimePeriod);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(100, 23);
			this.pnlHeader.TabIndex = 0;
			// 
			// lblTimePeriod
			// 
			this.lblTimePeriod.BackColor = System.Drawing.Color.GreenYellow;
			this.lblTimePeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblTimePeriod.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblTimePeriod.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblTimePeriod.Location = new System.Drawing.Point(0, 0);
			this.lblTimePeriod.Name = "lblTimePeriod";
			this.lblTimePeriod.TabIndex = 0;
			this.lblTimePeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtInput
			// 
			this.txtInput.BackColor = System.Drawing.Color.LightCyan;
			this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtInput.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtInput.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtInput.Location = new System.Drawing.Point(0, 0);
			this.txtInput.Multiline = true;
			this.txtInput.Name = "txtInput";
			this.txtInput.ReadOnly = true;
			this.txtInput.Size = new System.Drawing.Size(100, 24);
			this.txtInput.TabIndex = 1;
			this.txtInput.Text = "";
			this.txtInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtDefects
			// 
			this.txtDefects.BackColor = System.Drawing.Color.LavenderBlush;
			this.txtDefects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDefects.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtDefects.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtDefects.Location = new System.Drawing.Point(0, 24);
			this.txtDefects.Multiline = true;
			this.txtDefects.Name = "txtDefects";
			this.txtDefects.ReadOnly = true;
			this.txtDefects.Size = new System.Drawing.Size(100, 24);
			this.txtDefects.TabIndex = 2;
			this.txtDefects.Text = "";
			this.txtDefects.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtFPYGoal
			// 
			this.txtFPYGoal.BackColor = System.Drawing.Color.AliceBlue;
			this.txtFPYGoal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFPYGoal.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtFPYGoal.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtFPYGoal.Location = new System.Drawing.Point(0, 48);
			this.txtFPYGoal.Multiline = true;
			this.txtFPYGoal.Name = "txtFPYGoal";
			this.txtFPYGoal.Size = new System.Drawing.Size(100, 24);
			this.txtFPYGoal.TabIndex = 3;
			this.txtFPYGoal.Text = "";
			this.txtFPYGoal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtFPYGoal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFPYGoal_KeyPress);
			this.txtFPYGoal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFPYGoal_KeyUp);
			// 
			// txtActual
			// 
			this.txtActual.BackColor = System.Drawing.Color.White;
			this.txtActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtActual.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtActual.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtActual.Location = new System.Drawing.Point(0, 72);
			this.txtActual.Multiline = true;
			this.txtActual.Name = "txtActual";
			this.txtActual.ReadOnly = true;
			this.txtActual.Size = new System.Drawing.Size(100, 24);
			this.txtActual.TabIndex = 4;
			this.txtActual.Text = "";
			this.txtActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// pnlCenter
			// 
			this.pnlCenter.Controls.Add(this.txtFPYGoal);
			this.pnlCenter.Controls.Add(this.txtDefects);
			this.pnlCenter.Controls.Add(this.txtInput);
			this.pnlCenter.Controls.Add(this.txtActual);
			this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlCenter.Location = new System.Drawing.Point(0, 0);
			this.pnlCenter.Name = "pnlCenter";
			this.pnlCenter.Size = new System.Drawing.Size(100, 96);
			this.pnlCenter.TabIndex = 6;
			// 
			// pnlFill
			// 
			this.pnlFill.Controls.Add(this.pnlBottom);
			this.pnlFill.Controls.Add(this.pnlCenter);
			this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlFill.Location = new System.Drawing.Point(0, 23);
			this.pnlFill.Name = "pnlFill";
			this.pnlFill.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.pnlFill.Size = new System.Drawing.Size(100, 120);
			this.pnlFill.TabIndex = 7;
			// 
			// pnlBottom
			// 
			this.pnlBottom.Controls.Add(this.txtTotalActual);
			this.pnlBottom.Controls.Add(this.txtGoal);
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlBottom.Location = new System.Drawing.Point(0, 96);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size(100, 24);
			this.pnlBottom.TabIndex = 7;
			// 
			// txtTotalActual
			// 
			this.txtTotalActual.BackColor = System.Drawing.Color.White;
			this.txtTotalActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtTotalActual.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtTotalActual.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtTotalActual.Location = new System.Drawing.Point(0, 0);
			this.txtTotalActual.Multiline = true;
			this.txtTotalActual.Name = "txtTotalActual";
			this.txtTotalActual.ReadOnly = true;
			this.txtTotalActual.Size = new System.Drawing.Size(100, 24);
			this.txtTotalActual.TabIndex = 1;
			this.txtTotalActual.Text = "";
			this.txtTotalActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtGoal
			// 
			this.txtGoal.BackColor = System.Drawing.Color.Azure;
			this.txtGoal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtGoal.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtGoal.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtGoal.Location = new System.Drawing.Point(0, -6);
			this.txtGoal.Multiline = true;
			this.txtGoal.Name = "txtGoal";
			this.txtGoal.Size = new System.Drawing.Size(100, 30);
			this.txtGoal.TabIndex = 0;
			this.txtGoal.Text = "";
			this.txtGoal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtGoal.Visible = false;
			this.txtGoal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFPYGoal_KeyPress);
			this.txtGoal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtGoal_KeyUp);
			// 
			// UCWatchPanelCenter
			// 
			this.Controls.Add(this.pnlFill);
			this.Controls.Add(this.pnlHeader);
			this.Name = "UCWatchPanelCenter";
			this.Size = new System.Drawing.Size(100, 143);
			this.pnlHeader.ResumeLayout(false);
			this.pnlCenter.ResumeLayout(false);
			this.pnlFill.ResumeLayout(false);
			this.pnlBottom.ResumeLayout(false);
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

		#region Property
		public string TPCode;

		public string TPHeader
		{
			get
			{
				return this.lblTimePeriod.Text;
			}
			set
			{
				this.lblTimePeriod.Text = value;
			}
		}

		public decimal Input
		{
			get
			{
				try
				{
					return decimal.Parse(this.txtInput.Text);
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				this.txtInput.Text = value.ToString();
			}
		}

		public decimal Defects
		{
			get
			{
				try
				{
					return decimal.Parse(this.txtDefects.Text);
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				this.txtDefects.Text = value.ToString();
			}
		}

		public decimal FPYGoal
		{
			get
			{
				try
				{
					return decimal.Parse(this.txtFPYGoal.Text);
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				this.txtFPYGoal.Text = value.ToString();
			}
		}

		public decimal Actual
		{
			get
			{
				try
				{
					return decimal.Parse(this.txtActual.Text);
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				this.txtActual.Text = value.ToString("0.00");

				CompareActual();
			}
		}

		public decimal Goal
		{
			get
			{
				try
				{
					return decimal.Parse(this.txtGoal.Text);
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				this.txtGoal.Text = value.ToString();
			}
		}

		public decimal TotalActual
		{
			get
			{
				try
				{
					return decimal.Parse(this.txtTotalActual.Text);
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				this.txtTotalActual.Text = value.ToString("0.00");

				CompareTotalActual();
			}
		}
		#endregion

		private void CompareActual()
		{
			if(this.Actual == 0)
			{
				this.txtActual.BackColor = Color.White;
				this.txtActual.ForeColor = Color.Black;
				return;
			}

			if(this.Actual < this.FPYGoal)
			{
				this.txtActual.BackColor = Color.Red;
				this.txtActual.ForeColor = Color.White;
			}
			else
			{
				this.txtActual.BackColor = Color.White;
				this.txtActual.ForeColor = Color.Black;
			}
		}

		private void CompareTotalActual()
		{
			if(this.TotalActual == 0)
			{
				this.txtTotalActual.BackColor = Color.White;
				this.txtTotalActual.ForeColor = Color.Black;
				return;
			}

			if(this.TotalActual < this.Goal)
			{
				this.txtTotalActual.BackColor = Color.Red;
				this.txtTotalActual.ForeColor = Color.White;
			}
			else
			{
				this.txtTotalActual.BackColor = Color.White;
				this.txtTotalActual.ForeColor = Color.Black;
			}
		}

		private ICalculateStrategy Stategy;

		public void SetCalculateStrategy(ICalculateStrategy Stategy)
		{
			this.Stategy = Stategy;
		}

		public void CalculateActual()
		{
			if(Stategy != null)
			{
				decimal actual = this.Stategy.Calculate(this.Input,this.Defects,this.FPYGoal);

				if(actual == -1)
					this.txtActual.Text = "";
				else
					this.Actual = actual;
			}
		}

		public void ResetControlValue()
		{
			this.txtDefects.Text = "";
			this.txtInput.Text = "";
			this.txtTotalActual.Text = "";
			this.txtActual.Text = "";
		}

		private void txtGoal_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(!this.IsNumeric(txtGoal.Text))
			{
				txtGoal.Text = "";
				return;
			}

			CompareTotalActual();
		}

		private void txtFPYGoal_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(!this.IsNumeric(txtFPYGoal.Text))
			{
				txtFPYGoal.Text = "";
				return;
			}

			CompareActual();
		}

		private void txtFPYGoal_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
//			if (!("0123456789.-".IndexOf(e.KeyChar) >= 0)||(e.KeyChar==8))
//			{
//				e.Handled =true;
//			}
		}

		/// <summary>
		/// 判断是否是数字
		/// </summary>
		/// <param name="str">字符串</param>
		/// <returns></returns>
		private bool IsNumeric(string str) 
		{
			try
			{
				decimal.Parse(str);
				return true;
			}
			catch
			{
				return false;
			}
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

		public void SetFPYGoal(decimal fpygoal)
		{
			this.FPYGoal = fpygoal;
		}
	}
}
