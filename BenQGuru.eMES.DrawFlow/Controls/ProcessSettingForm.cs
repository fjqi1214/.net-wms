/***********************************************************************
 * Module:  ProcessSettingForm.cs
 * Author:  Administrator
 * Purpose: Definition of the Class Flow.Controls.ProcessSettingForm
 ***********************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BenQGuru.eMES.DrawFlow.Data;
using System.Data;

namespace BenQGuru.eMES.DrawFlow.Controls
{
   /// <summary>
   /// ProcessSettingForm 的摘要说明。
   /// </summary>
	public class ProcessSettingForm : Form
	{
		/// <summary>
		/// </summary>
		public ProcessSettingForm(FunctionButton fb)
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			fFunctionButton = fb;

			//baseBussiness = new BaseBusiness();
		}
   
		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose(bool disposing)
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
   
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.tbID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lbIn = new System.Windows.Forms.ListBox();
			this.lbOut = new System.Windows.Forms.ListBox();
			this.cbName = new System.Windows.Forms.ComboBox();
			this.labInspectPer = new System.Windows.Forms.Label();
			this.ntxtInspectPer = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "进程名称：";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbID
			// 
			this.tbID.Location = new System.Drawing.Point(88, 48);
			this.tbID.Name = "tbID";
			this.tbID.ReadOnly = true;
			this.tbID.Size = new System.Drawing.Size(232, 21);
			this.tbID.TabIndex = 3;
			this.tbID.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "进程ID：";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "源进程集：";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 144);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 23);
			this.label4.TabIndex = 6;
			this.label4.Text = "目标进程集：";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(39, 241);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(120, 40);
			this.btnOK.TabIndex = 8;
			this.btnOK.Text = "保存";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(175, 241);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(120, 40);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "取消";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lbIn
			// 
			this.lbIn.BackColor = System.Drawing.SystemColors.Control;
			this.lbIn.ItemHeight = 12;
			this.lbIn.Location = new System.Drawing.Point(88, 80);
			this.lbIn.Name = "lbIn";
			this.lbIn.Size = new System.Drawing.Size(232, 52);
			this.lbIn.Sorted = true;
			this.lbIn.TabIndex = 10;
			// 
			// lbOut
			// 
			this.lbOut.BackColor = System.Drawing.SystemColors.Control;
			this.lbOut.ItemHeight = 12;
			this.lbOut.Location = new System.Drawing.Point(88, 144);
			this.lbOut.Name = "lbOut";
			this.lbOut.Size = new System.Drawing.Size(232, 52);
			this.lbOut.Sorted = true;
			this.lbOut.TabIndex = 11;
			// 
			// cbName
			// 
			this.cbName.BackColor = System.Drawing.SystemColors.Window;
			this.cbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbName.Location = new System.Drawing.Point(88, 16);
			this.cbName.Name = "cbName";
			this.cbName.Size = new System.Drawing.Size(232, 20);
			this.cbName.TabIndex = 12;
			// 
			// labInspectPer
			// 
			this.labInspectPer.Location = new System.Drawing.Point(8, 207);
			this.labInspectPer.Name = "labInspectPer";
			this.labInspectPer.Size = new System.Drawing.Size(80, 23);
			this.labInspectPer.TabIndex = 13;
			this.labInspectPer.Text = "抽检比例：";
			this.labInspectPer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ntxtInspectPer
			// 
			this.ntxtInspectPer.Location = new System.Drawing.Point(88, 208);
			this.ntxtInspectPer.Name = "ntxtInspectPer";
			this.ntxtInspectPer.Size = new System.Drawing.Size(104, 21);
			this.ntxtInspectPer.TabIndex = 14;
			this.ntxtInspectPer.Text = "0";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(200, 207);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(120, 23);
			this.label5.TabIndex = 15;
			this.label5.Text = "输入0～1之间的小数";
			// 
			// ProcessSettingForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(338, 295);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.ntxtInspectPer);
			this.Controls.Add(this.tbID);
			this.Controls.Add(this.labInspectPer);
			this.Controls.Add(this.cbName);
			this.Controls.Add(this.lbOut);
			this.Controls.Add(this.lbIn);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProcessSettingForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "进程属性设置对话框";
			this.Load += new System.EventHandler(this.ProcessSettingForm_Load);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.TextBox ntxtInspectPer;
		private System.Windows.Forms.Label label5;
      		

		//private BaseBusiness baseBussiness = null;

		/// <summary>
		/// </summary>
		private void ProcessSettingForm_Load(object sender, System.EventArgs e)
		{
			
			//通过按钮所在的控件的Tag属性传递数据表
			if(fFunctionButton.Parent.Tag == null)
				throw new Exception("请设置按钮所在的控件的Tag属性以传递下拉数据表");
			DataTable dtList = fFunctionButton.Parent.Tag as DataTable;
			int index = 0;
			string processName = fFunctionButton.ProcessName.ToUpper().Trim();
			/***************************Begin****************************/
			//判断是新增加的节点还是修改的节点
			if (processName == null || processName.Length <= 0)
			{
				isNewNode = true;
			}
			else
			{
				isNewNode = false;
			}
			/***************************End******************************/
			foreach(DataRow dr in dtList.Rows)
			{
				cbName.Items.Add(string.Format("{0}[{1}]",dr["CD_VALUE"],dr["CD_KEY"]));
			}	
      
			foreach(string item in cbName.Items)
			{
				if(item.ToUpper().Trim().StartsWith(processName + "["))
				{
					//设置进程名称
					cbName.SelectedIndex = index;
					break;
				}
				else
				{
					index++;
				}
			}
      	
      
			tbID.Text = fFunctionButton.ProcessID.ToString().ToUpper();

			/***************************Begin****************************/
			//抽检类型设置
			ntxtInspectPer.Enabled = fFunctionButton.IsInspectStyle;
			if (fFunctionButton.InspectPer >= 0 && fFunctionButton.InspectPer <= 1)
			{	
				ntxtInspectPer.Text = fFunctionButton.InspectPer.ToString();
			}
			else
			{				
				ntxtInspectPer.Text = "0";
			}
			/***************************End******************************/
      
			foreach(FlowButton flowBtn in fFunctionButton.InFlows)
			{
				foreach(FunctionButton funcBtn in flowBtn.FromProcesses)
				{
					//设置源进程
					lbIn.Items.Add(funcBtn);
				}
			}			
      
			//设置目标进程
			foreach(FlowButton flowBtn in fFunctionButton.OutFlows)
			{
				foreach(FunctionButton funcBtn in flowBtn.ToProcesses)
				{
					if(funcBtn is EndButton)
					{
						bool has = false;
						foreach(object obj in lbOut.Items)
						{
							if(obj is EndButton)
							{
								has = true;
							}
						}
						if(!has)
						{
							lbOut.Items.Add(funcBtn);
						}
					}
					else
					{
						lbOut.Items.Add(funcBtn);
					}
				}
			}
			//设置进程名称下拉框不可用
			if (cbName.Text.Length > 0)
			{
				//如果原来有值，则不允许修改
				cbName.Enabled = false;
			}
			else
			{
				cbName.Enabled = true;
			}
		}
      
		/// <summary>
		/// </summary>
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			try
			{				
				fFunctionButton.ProcessName = DataUtility.GetValueFromString(cbName.SelectedItem.ToString());
				fFunctionButton.ProcessCode = DataUtility.GetKeyFromString(cbName.SelectedItem.ToString());
				fFunctionButton.WorkFlowID = this.RouteCode;
				/***************************Begin****************************/
				//抽检类型、抽检比例
				if (isNewNode == false)
				{
					//isNewNode == false表示是修改节点，在修改节点的时候才保存抽检比例
					bool isValid = CheckInspectPer();
					if ( isValid == true)
					{
						fFunctionButton.InspectPer = decimal.Parse(ntxtInspectPer.Text);
						decimal inspectPer = decimal.Parse(ntxtInspectPer.Text);
						//保存抽检比例
						string fields = "SET WFP_INSPECT_PERCENT=" + inspectPer;
						string whereCond = "AND WORKFLOW_CODE='" + this.RouteCode + 
							"' AND WFP_CODE='" + fFunctionButton.ProcessID + "'";
						//baseBussiness.UpdataData("C_WORKFLOW_NODE", fields, whereCond);
					}
					else
					{
						return;
					}
				}
				/***************************End******************************/
				DialogResult = DialogResult.OK;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

          
		public bool CheckInspectPer()
		{
			try
			{
				if(decimal.Parse(ntxtInspectPer.Text) < 0)
				{
					MessageBox.Show("抽检比例不能小于零！");
					return false;
				}
				if(decimal.Parse(ntxtInspectPer.Text) > 1)
				{
					MessageBox.Show("抽检比例不能大于1！");
					return false;
				}
				return true;
			}
			catch(Exception ex)
			{
				MessageBox.Show("输入的字符非法，请重新输入！");
				return false;
			}
		}
      
		/// <summary>
		/// </summary>
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
      
		/// <summary>
		/// </summary>
		private void tbName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(DataUtility.IsEnterChar(e.KeyChar))
			{
				btnOK_Click(sender,e);
			}
		}
   
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListBox lbIn;
		private System.Windows.Forms.ListBox lbOut;
		private System.Windows.Forms.ComboBox cbName;
		private System.Windows.Forms.Label labInspectPer;
		private FunctionButton fFunctionButton = null;

		/***************************Begin****************************/
		//是否是新增加的节点
		bool isNewNode = false;
		private string routeCode = null;
		/// <summary>
		/// 路线Code
		/// </summary>
		public string RouteCode
		{
			get
			{
				return routeCode;
			}
			set 
			{
				routeCode = value;
			}
		}
		/***************************End******************************/

		
   
	}
}