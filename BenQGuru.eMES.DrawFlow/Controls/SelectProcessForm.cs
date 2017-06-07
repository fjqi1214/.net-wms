/***********************************************************************
 * Module:  SelectProcessForm.cs
 * Author:  Administrator
 * Purpose: Definition of the Class Flow.Controls.SelectProcessForm
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
   /// SelectProcessForm 的摘要说明。
   /// </summary>
   public class SelectProcessForm : Form
   {
      /// <summary>
      /// </summary>
      public SelectProcessForm(Control parentControl)
      {
      	//
      	// Windows 窗体设计器支持所必需的
      	//
      	InitializeComponent();
      
      	//
      	// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
      	//
      	fParentControl = parentControl;
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
          this.btnOK = new System.Windows.Forms.Button();
          this.cbList = new System.Windows.Forms.ComboBox();
          this.SuspendLayout();
          // 
          // btnOK
          // 
          this.btnOK.Dock = System.Windows.Forms.DockStyle.Top;
          this.btnOK.Location = new System.Drawing.Point(0, 0);
          this.btnOK.Name = "btnOK";
          this.btnOK.Size = new System.Drawing.Size(208, 30);
          this.btnOK.TabIndex = 0;
          this.btnOK.Text = "确 定";
          this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
          // 
          // cbList
          // 
          this.cbList.BackColor = System.Drawing.SystemColors.Control;
          this.cbList.Dock = System.Windows.Forms.DockStyle.Fill;
          this.cbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
          this.cbList.Location = new System.Drawing.Point(0, 30);
          this.cbList.Name = "cbList";
          this.cbList.Size = new System.Drawing.Size(208, 160);
          this.cbList.TabIndex = 1;
          // 
          // SelectProcessForm
          // 
          this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
          this.ClientSize = new System.Drawing.Size(208, 190);
          this.Controls.Add(this.cbList);
          this.Controls.Add(this.btnOK);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "SelectProcessForm";
          this.ShowInTaskbar = false;
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "选择连接点";
          this.Load += new System.EventHandler(this.SelectProcessForm_Load);
          this.ResumeLayout(false);

      }
      
      /// <summary>
      /// </summary>
      private void SelectProcessForm_Load(object sender, System.EventArgs e)
      {
      	foreach(Control ctrl in fParentControl.Controls)
      	{
      		if(ctrl is ProcessButton)
      		{
      			cbList.Items.Add(ctrl as ProcessButton);
      		}
      	}
      }
      
      /// <summary>
      /// </summary>
      private void cbList_DoubleClick(object sender, System.EventArgs e)
      {
      	
      }
      
      /// <summary>
      /// </summary>
      private void btnOK_Click(object sender, System.EventArgs e)
      {
      	if(SelectedButton == null)
      	{
      		MessageBox.Show("请选择目标进程");
      		cbList.Focus();
      	}
      	else
      	{
      		DialogResult = DialogResult.OK;
      	}
      }
   
      /// <summary>
      /// 必需的设计器变量。
      /// </summary>
      private System.ComponentModel.Container components = null;
      private System.Windows.Forms.Button btnOK;
      private System.Windows.Forms.ComboBox cbList;
      private Control fParentControl = null;
   
      public ProcessButton SelectedButton
      {
         get
         {
         	return cbList.SelectedItem as ProcessButton;
         }
      }
   
   }
}