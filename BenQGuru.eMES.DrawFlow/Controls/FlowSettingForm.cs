/***********************************************************************
 * Module:  FlowSettingForm.cs
 * Author:  Administrator
 * Purpose: Definition of the Class Flow.Controls.FlowSettingForm
 ***********************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BenQGuru.eMES.DrawFlow.Data;

namespace BenQGuru.eMES.DrawFlow.Controls
{
   /// <summary>
   /// FlowSettingForm 的摘要说明。
   /// </summary>
   public class FlowSettingForm : Form
   {
      /// <summary>
      /// </summary>
      public FlowSettingForm(FlowButton fb)
      {
      	//
      	// Windows 窗体设计器支持所必需的
      	//
      	InitializeComponent();
      
      	//
      	// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
      	//
      	fFlowButton = fb;
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
      	this.btnCancel = new System.Windows.Forms.Button();
      	this.btnOK = new System.Windows.Forms.Button();
      	this.cbTo = new System.Windows.Forms.ComboBox();
      	this.label4 = new System.Windows.Forms.Label();
      	this.cbFrom = new System.Windows.Forms.ComboBox();
      	this.label3 = new System.Windows.Forms.Label();
      	this.tbID = new System.Windows.Forms.TextBox();
      	this.label2 = new System.Windows.Forms.Label();
      	this.tbName = new System.Windows.Forms.TextBox();
      	this.label1 = new System.Windows.Forms.Label();
      	this.cbDoubleArrow = new System.Windows.Forms.CheckBox();
      	this.tbCond = new System.Windows.Forms.TextBox();
      	this.label5 = new System.Windows.Forms.Label();
      	this.SuspendLayout();
      	// 
      	// btnCancel
      	// 
      	this.btnCancel.Location = new System.Drawing.Point(184, 208);
      	this.btnCancel.Name = "btnCancel";
      	this.btnCancel.Size = new System.Drawing.Size(120, 40);
      	this.btnCancel.TabIndex = 19;
      	this.btnCancel.Text = "取消";
      	this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      	// 
      	// btnOK
      	// 
      	this.btnOK.Location = new System.Drawing.Point(40, 208);
      	this.btnOK.Name = "btnOK";
      	this.btnOK.Size = new System.Drawing.Size(120, 40);
      	this.btnOK.TabIndex = 18;
      	this.btnOK.Text = "保存";
      	this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      	// 
      	// cbTo
      	// 
      	this.cbTo.BackColor = System.Drawing.SystemColors.Window;
      	this.cbTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      	this.cbTo.Location = new System.Drawing.Point(88, 112);
      	this.cbTo.Name = "cbTo";
      	this.cbTo.Size = new System.Drawing.Size(232, 20);
      	this.cbTo.Sorted = true;
      	this.cbTo.TabIndex = 17;
      	// 
      	// label4
      	// 
      	this.label4.Location = new System.Drawing.Point(8, 112);
      	this.label4.Name = "label4";
      	this.label4.Size = new System.Drawing.Size(80, 23);
      	this.label4.TabIndex = 16;
      	this.label4.Text = "目标进程：";
      	this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      	// 
      	// cbFrom
      	// 
      	this.cbFrom.BackColor = System.Drawing.SystemColors.Window;
      	this.cbFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      	this.cbFrom.Location = new System.Drawing.Point(88, 80);
      	this.cbFrom.Name = "cbFrom";
      	this.cbFrom.Size = new System.Drawing.Size(232, 20);
      	this.cbFrom.Sorted = true;
      	this.cbFrom.TabIndex = 15;
      	// 
      	// label3
      	// 
      	this.label3.Location = new System.Drawing.Point(8, 80);
      	this.label3.Name = "label3";
      	this.label3.Size = new System.Drawing.Size(80, 23);
      	this.label3.TabIndex = 14;
      	this.label3.Text = "源进程：";
      	this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      	// 
      	// tbID
      	// 
      	this.tbID.Location = new System.Drawing.Point(88, 48);
      	this.tbID.Name = "tbID";
      	this.tbID.ReadOnly = true;
      	this.tbID.Size = new System.Drawing.Size(232, 21);
      	this.tbID.TabIndex = 13;
      	this.tbID.Text = "";
      	// 
      	// label2
      	// 
      	this.label2.Location = new System.Drawing.Point(8, 48);
      	this.label2.Name = "label2";
      	this.label2.Size = new System.Drawing.Size(80, 23);
      	this.label2.TabIndex = 12;
      	this.label2.Text = "流程ID：";
      	this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      	// 
      	// tbName
      	// 
      	this.tbName.Location = new System.Drawing.Point(88, 16);
      	this.tbName.Name = "tbName";
      	this.tbName.Size = new System.Drawing.Size(232, 21);
      	this.tbName.TabIndex = 11;
      	this.tbName.Text = "";
      	this.tbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbName_KeyPress);
      	// 
      	// label1
      	// 
      	this.label1.Location = new System.Drawing.Point(8, 16);
      	this.label1.Name = "label1";
      	this.label1.Size = new System.Drawing.Size(80, 23);
      	this.label1.TabIndex = 10;
      	this.label1.Text = "流程名称：";
      	this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      	// 
      	// cbDoubleArrow
      	// 
      	this.cbDoubleArrow.Enabled = false;
      	this.cbDoubleArrow.Location = new System.Drawing.Point(88, 176);
      	this.cbDoubleArrow.Name = "cbDoubleArrow";
      	this.cbDoubleArrow.TabIndex = 20;
      	this.cbDoubleArrow.Text = "双向箭头";
      	// 
      	// tbCond
      	// 
      	this.tbCond.Location = new System.Drawing.Point(88, 144);
      	this.tbCond.Name = "tbCond";
      	this.tbCond.Size = new System.Drawing.Size(232, 21);
      	this.tbCond.TabIndex = 22;
      	this.tbCond.Text = "";
      	// 
      	// label5
      	// 
      	this.label5.Location = new System.Drawing.Point(8, 144);
      	this.label5.Name = "label5";
      	this.label5.Size = new System.Drawing.Size(80, 23);
      	this.label5.TabIndex = 21;
      	this.label5.Text = "流转条件：";
      	this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      	// 
      	// FlowSettingForm
      	// 
      	this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
      	this.ClientSize = new System.Drawing.Size(330, 264);
      	this.Controls.Add(this.tbCond);
      	this.Controls.Add(this.label5);
      	this.Controls.Add(this.cbDoubleArrow);
      	this.Controls.Add(this.btnCancel);
      	this.Controls.Add(this.btnOK);
      	this.Controls.Add(this.cbTo);
      	this.Controls.Add(this.label4);
      	this.Controls.Add(this.cbFrom);
      	this.Controls.Add(this.label3);
      	this.Controls.Add(this.tbID);
      	this.Controls.Add(this.tbName);
      	this.Controls.Add(this.label2);
      	this.Controls.Add(this.label1);
      	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      	this.MaximizeBox = false;
      	this.MinimizeBox = false;
      	this.Name = "FlowSettingForm";
      	this.ShowInTaskbar = false;
      	this.Text = "流程属性设置窗体";
      	this.Load += new System.EventHandler(this.FlowSettingForm_Load);
      	this.ResumeLayout(false);
      
      }
      
      /// <summary>
      /// </summary>
      private void btnOK_Click(object sender, System.EventArgs e)
      {
      	fFlowButton.ArrowName = tbName.Text;
      	
      	fFlowButton.FromProcesses.Clear();
      	fFlowButton.FromProcesses.Add(cbFrom.SelectedItem as FunctionButton);
      	(cbFrom.SelectedItem as FunctionButton).OutFlows.Add(fFlowButton);
      
      	fFlowButton.ToProcesses.Clear();
      	fFlowButton.ToProcesses.Add(cbTo.SelectedItem as FunctionButton);
      	(cbTo.SelectedItem as FunctionButton).InFlows.Add(fFlowButton);
      	DialogResult = DialogResult.OK;
      }
      
      /// <summary>
      /// </summary>
      private void btnCancel_Click(object sender, System.EventArgs e)
      {
      	DialogResult = DialogResult.Cancel;
      }
      
      /// <summary>
      /// </summary>
      private void FlowSettingForm_Load(object sender, System.EventArgs e)
      {
      	tbName.Text = fFlowButton.ArrowName;
      	cbDoubleArrow.Checked = fFlowButton.DoubleArrow;
      	tbID.Text = fFlowButton.ArrowID.ToString().ToUpper();
      	tbCond.Text = fFlowButton.Condition;
      
      	if(fFlowButton.FromProcesses.Count == 0)
      	{
      		//还没有设置，要全部的
      	}			
      	foreach(Control ctrl in fFlowButton.Parent.Controls)
      	{
      		if(ctrl is FunctionButton)
      		{
      			if(!(ctrl is EndButton))
      			{
      				cbFrom.Items.Add((ctrl as FunctionButton));		
      			}
      	
      			if(!(ctrl is StartButton))
      			{
      				//如果是结束标记，只要增加一个就可以了
      				if(ctrl is EndButton)
      				{
      					bool has = false;
      					foreach(object obj in cbTo.Items)
      					{
      						if(obj is EndButton)
      						{
      							has = true;
      							break;
      						}
      					}
      					if(!has)
      					{
      						cbTo.Items.Add(ctrl as FunctionButton);
      					}
      				}
      				else
      				{
      					cbTo.Items.Add((ctrl as FunctionButton));					
      				}
      			}
      		}
      	}	
      	if(fFlowButton.FromProcesses.Count > 0)
      	{
      		cbFrom.SelectedItem = fFlowButton.FromProcesses[0];
      	}
      	if(fFlowButton.ToProcesses.Count > 0)
      	{
      		cbTo.SelectedItem = fFlowButton.ToProcesses[0];
      	}
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
   
      private System.Windows.Forms.Button btnCancel;
      private System.Windows.Forms.Button btnOK;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.TextBox tbID;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.TextBox tbName;
      private System.Windows.Forms.Label label1;
      /// <summary>
      /// 必需的设计器变量。
      /// </summary>
      private System.ComponentModel.Container components = null;
      private System.Windows.Forms.ComboBox cbTo;
      private System.Windows.Forms.ComboBox cbFrom;
      private System.Windows.Forms.CheckBox cbDoubleArrow;
      private System.Windows.Forms.TextBox tbCond;
      private System.Windows.Forms.Label label5;
      private FlowButton fFlowButton = null;
   
   }
}